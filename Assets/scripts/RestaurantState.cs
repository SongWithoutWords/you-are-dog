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

    private string MakeCaughtText()
    {
        return "You've been caught!\n\n You consumed " + FindObjectOfType<PlayerEat>().calories + " calories!";
    }

    public void NotifyPlayerCaught()
    {
        alertState = AlertState.Caught;

        gotAwayText.color = new Color(gotAwayText.color.r, gotAwayText.color.g, gotAwayText.color.b, 255);
        gotAwayText.text = MakeCaughtText();

        dogText.text.text = "";

        Invoke("LoadMainMenu", 5.0f);
    }

    private string MakeGotAwayText(int baseCalories, int bonusCalories)
    {
        return "You got away!\n\n You consumed " +  baseCalories + " calories!\n" +  "Escape bonus: " + bonusCalories + "\nTotal: " + (baseCalories+bonusCalories);
    }

    public void NotifyPlayerGotAway()
    {
        alertState = AlertState.GotAway;
        PlayerEat playerEat = FindObjectOfType<PlayerEat>();
        int escapeBonus = (int)(playerEat.calories * escapeBonusRatio);

        gotAwayText.color = new Color(gotAwayText.color.r, gotAwayText.color.g, gotAwayText.color.b, 255);
        gotAwayText.text = MakeGotAwayText(playerEat.calories, escapeBonus);

        playerEat.calories += escapeBonus;

        dogText.text.text = "";

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
            dogText.text.text = "YOU MUST ESCAPE";
            dogText.startTime = Time.time;

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

            switch (alertState)
            {
                case AlertState.Alert:
                    dogText.text.text = "THEY GROW SUSPICIOUS";
                    dogText.startTime = Time.time;
                    break;
                case AlertState.Aware:
                    dogText.text.text = "THEY KNOW YOU ARE DOG";
                    dogText.startTime = Time.time;
                    break;
                default: break;
            }
        }
        // We can only transition down from Alert. Once they're aware, there's no going back.
        else if (alertState <= AlertState.Alert)
        {
            dogText.text.text = "";

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
