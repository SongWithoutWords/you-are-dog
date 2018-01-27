using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum AlertState
{
    calm,
    alert,
    aware,
    gtfo
}

public class RestaurantState : MonoBehaviour
{
    public int alertDecayRate = 1;
    public int alertThreshold = 10000;

    public AlertState alert = AlertState.calm;
    public int alertLevel = 0;

    public Text alertText;

    // Use this for initialization
    void Start() {}

    public void ReduceAlert()
    {
        alertLevel -= alertDecayRate;
        if ((alert == AlertState.calm || alert == AlertState.alert) && alertLevel < 0)
        {
            alertLevel = 0;
        }
    }

    public void UpdateState()
    {
        if (alertLevel >= alertThreshold)
        {
            alert = AlertState.alert;
        }
        else
        {
            alert = AlertState.calm;
        }
    }

    public void UpdateText()
    {
        alertText.text = "Alert level: " + alert.ToString() + " " + alertLevel.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        ReduceAlert();
        UpdateState();
        UpdateText();
    }
}
