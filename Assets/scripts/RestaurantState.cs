using UnityEngine;
using System.Collections;

public enum AlertState
{
    calm,
    alert,
    aware,
    gtfo
}

public class RestaurantState : MonoBehaviour {

    public int alertDecayRate = 1;
    public int alertThreshold = 50;

    public AlertState alert;
    public int alertLevel;

	// Use this for initialization
	void Start () {
        alert = AlertState.calm;
        alertLevel = 0;
	}
	
    public void reduceAlert() {
        alertLevel -= alertDecayRate;
    }

    public void updateState()
    {
        if (alertLevel >= alertThreshold)
        {
            alert = AlertState.alert;
        }
    }

	// Update is called once per frame
	void Update () {
        reduceAlert();

        updateState();
	}
}
