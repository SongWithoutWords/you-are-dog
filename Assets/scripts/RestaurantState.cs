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

    [HideInInspector]
    public float alertLevel = 0;

    void Start()
    {
        CollisionDispatcher[] dispatchers = FindObjectsOfType<CollisionDispatcher>();
        foreach (var dispatcher in dispatchers)
        {
            dispatcher.OnCollisionEnter += OnCollision;
        }
    }

    void OnCollision(Collision2D collision, float acceleration)
    {
        //alertLevel += jostled.alertAmount;
    }

    AlertState LevelToState(float level)
    {
        return
            level >= thresholdEscape ? AlertState.Escape :
            level >= thresholdAware ? AlertState.Aware :
            level >= thresholdAlert ? AlertState.Alert :
            AlertState.Relaxed;
    }

    float Threshold(AlertState state)
    {
        return
            state >= AlertState.Escape ? thresholdEscape :
            state >= AlertState.Aware ? thresholdAware :
            state >= AlertState.Alert ? thresholdAlert :
            0;
    }

    void ReduceAlert()
    {
        alertLevel -= alertDecayPerSecond * Time.fixedDeltaTime;
        alertLevel = Mathf.Max(0, alertLevel);
    }

    void UpdateState()
    {
        var nextState = LevelToState(alertLevel);
        if (nextState > alertState)
        {
            // Add half the threshold, to prevent rapid state transitions
            alertLevel += (Threshold(nextState) - Threshold(alertState)) / 2;
            alertState = nextState;
        }
        else if (alertState <= AlertState.Alert)
        {
            // We can only transition down from alert. 
            // Once they're aware, there's no going back
            alertState = nextState;
        }
    }

    void UpdateText()
    {
        alertText.text = "Alert level: " + alertState + " " + alertLevel;
    }

    // Update is called once per frame
    void Update()
    {
        ReduceAlert();
        UpdateState();
        UpdateText();
    }
}
