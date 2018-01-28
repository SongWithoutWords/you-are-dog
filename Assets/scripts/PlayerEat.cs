using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEat : MonoBehaviour {

    int calories;

	// Use this for initialization
	void Start () {
        calories = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Eat food on collision
    void OnTriggerEnter2D(Collider2D c)
    {
        Food food = c.gameObject.GetComponent<Food>();
        if (food != null)
        {
            calories += food.calories;
            Destroy(food.gameObject);
        }
    }
}
