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

    void EatFood(Food food)
    {
        calories += food.calories;
        Transform t = food.GetComponent<Transform>();
        Instantiate(food.wreck, t.position, t.rotation);
        Destroy(food.gameObject);
    }

    // Eat food on collision
    void OnTriggerEnter2D(Collider2D c)
    {
        Food food = c.gameObject.GetComponent<Food>();
        if (food != null)
        {
            EatFood(food);
        }
    }
}
