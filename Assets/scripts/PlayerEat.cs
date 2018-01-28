using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEat : MonoBehaviour {

    public int calories;
    public Text calorieText;

	// Use this for initialization
	void Start () {
        calories = 0;
	}

    void UpdateText()
    {
        calorieText.text = "Calories Consumed: " + calories;
    }

	// Update is called once per frame
	void Update () 
    {
        UpdateText();	
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
