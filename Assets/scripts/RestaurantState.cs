using UnityEngine;
using UnityEngine.UI;

public enum AlertState
{
    Relaxed,
    Alert,
    Aware,
    Escape
}

public class RestaurantState : MonoBehaviour
{
    public Text alertText;

    public float alertDecayPerSecond = 1;
    public float thresholdAlert = 20;
    public float thresholdAware = 40;
    public float thresholdEscape = 60;

    [HideInInspector]
    public AlertState alertState = AlertState.Relaxed;
    private float alertLevel = 0.0f;
    
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
        var nextState = LevelToState(alertLevel);
        if (nextState > alertState)
        {
            // Add half the threshold, to prevent rapid state transitions
            alertLevel = Threshold(nextState) + ((Threshold(nextState) - Threshold(alertState)) / 2);
            alertState = nextState;
        }
        else if (alertState <= AlertState.Alert)
        {
            // We can only transition down from alert. 
            // Once they're aware, there's no going back
            alertState = nextState;
        }
    }

    private void UpdateAlertText()
    {
        alertText.text = "Alert level: " + alertState + " " + alertLevel;
    }

    // Update is called once per frame
    void Update()
    {
        DecayAlert();
        UpdateAlertState();
        UpdateAlertText();
    }
}
