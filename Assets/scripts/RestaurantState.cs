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

    public AlertState alert;

	// Use this for initialization
	void Start () {
        alert = AlertState.calm;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
