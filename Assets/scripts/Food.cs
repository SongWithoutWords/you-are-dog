using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

    public int calories;
    public GameObject wreck;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void onDestroy()
    {
        Transform t = GetComponent<Transform>();
        Instantiate(wreck, t.position, t.rotation);
    }
}
