using UnityEngine;
using System.Collections;

public class RestaurantState : MonoBehaviour {

    enum AlertState
    {
        calm,
        alert,
        aware,
        gtfo
    }

    AlertState alert;

	// Use this for initialization
	void Start () {
        alert = AlertState.calm;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
