using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum AlertState
{
    Relaxed,
    Alert,
    Aware,
    Escape,
    Caught,
    GotAway,
}

public class RestaurantState : MonoBehaviour
{
    public Text alertText;
    public GameObject dogCatcherPrefab;
    public string mainMenuScene;

    public float alertDecayPerSecond = 1;
    public float thresholdAlert = 20;
    public float thresholdAware = 40;
    public float thresholdEscape = 60;

    public float escapeBonusRatio;

    [HideInInspector]
    public AlertState alertState = AlertState.Relaxed;
    private float alertLevel = 0.0f;

    public YouAreDogText dogText;
    public Text gotAwayText;

    void Start()
    {
        dogText.enabled = false;
    }

    public void AddAlert(float amount)
    {
        // Alert can only be added when the AlertState is Relaxed or Alert, not Aware or Escape.
        if (alertState <= AlertState.Alert)
        {
            alertLevel += amount;
        }
    }

    public void AddCallProgress(float amount)
    {
        // Call progress can only be added when the AlertState is Aware.
        if (alertState == AlertState.Aware)
        {
            alertLevel += amount;
        }
    }

    public void NotifyPlayerCaught()
    {
        alertState = AlertState.Caught;
        Invoke("LoadMainMenu", 5.0f);

    }

    string MakeWinText(int baseCalories, int bonusCalories)
    {

        return "You got away!\n\n You consumed " +  baseCalories + " calories!\n" +  "Escape bonus: " + bonusCalories + "\nTotal: " + (baseCalories+bonusCalories);
    }

    string MakeLoseText()
    {
        return "You've been caught!\n\n You consumed " + FindObjectOfType<PlayerEat>().calories + " calories!";
    }

    public void NotifyPlayerGotAway()
    {
        alertState = AlertState.GotAway;
        PlayerEat playerEat = FindObjectOfType<PlayerEat>();
        int escapeBonus = (int) ((float) playerEat.calories * escapeBonusRatio);

        gotAwayText.color = new Color(gotAwayText.color.r, gotAwayText.color.g, gotAwayText.color.b, 255);
        gotAwayText.text = MakeWinText(playerEat.calories, escapeBonus);

        playerEat.calories += escapeBonus;
        if (dogText.text != null)
        {
            dogText.text.color = new Color(0, 0, 0, 0);        
        }
        
        dogText.enabled = false;
        Invoke("LoadMainMenu", 5.0f);
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene, LoadSceneMode.Single);
    }

    private AlertState LevelToState(float level)
    {
        return
            level >= thresholdEscape ? AlertState.Escape :
            level >= thresholdAware ? AlertState.Aware :
            level >= thresholdAlert ? AlertState.Alert :
            AlertState.Relaxed;
    }

    private float Threshold(AlertState state)
    {
        return
            state >= AlertState.Escape ? thresholdEscape :
            state >= AlertState.Aware ? thresholdAware :
            state >= AlertState.Alert ? thresholdAlert :
            0;
    }

    private void DecayAlert()
    {
        if (alertState <= AlertState.Alert)
        {
            alertLevel -= alertDecayPerSecond * Time.fixedDeltaTime;
            alertLevel = Mathf.Max(0, alertLevel);
        }
    }

    private void UpdateAlertState()
    {
        // Only update the alert state if it's below Escape.
        if (alertState >= AlertState.Escape)
        {
            return;
        }

        // Get the next state based on the alert level. If it hasn't changed, early out.
        var nextState = LevelToState(alertLevel);
        if (nextState == alertState)
        {
            return;
        }

        // Spawn the dog catcher when Escape begins.
        if (nextState == AlertState.Escape)
        {
            alertState = AlertState.Escape;
            
            var exit = GameObject.FindGameObjectWithTag("FrontDoor");
            if (exit != null && dogCatcherPrefab != null)
            {
                Instantiate(dogCatcherPrefab, exit.transform.position, exit.transform.rotation);
            }

        }
        // The state is always allowed to go up.
        else if (nextState > alertState)
        {
            // Add half the threshold, to prevent rapid state transitions
            alertLevel = Threshold(nextState) + ((Threshold(nextState) - Threshold(alertState)) / 2);
            alertState = nextState;
            if (alertState >= AlertState.Aware)
            {
                dogText.enabled = true;
                dogText.startTime = Time.time;
            }
        }
        // We can only transition down from Alert. Once they're aware, there's no going back.
        else if (alertState <= AlertState.Alert)
        {
            alertState = nextState;
        }
    }

    private void UpdateAlertText()
    {
        alertText.text = "Alert level: " + alertState + " " + alertLevel;
    }
    
    void Update()
    {
        DecayAlert();
        UpdateAlertState();
        UpdateAlertText();
    }
}
