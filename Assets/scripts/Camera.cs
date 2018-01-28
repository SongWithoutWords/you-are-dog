using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {
    public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Transform transform = GetComponent<Transform>();
        Transform playerTransform = player.GetComponent<Transform>();

        Vector3 newPosition = playerTransform.position;
        newPosition.z = transform.position.z;
        transform.position = newPosition;
	}
}
