using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YouAreDogText : MonoBehaviour {
    public Text text;
    public float startTime;
	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}

    float CalculateTextAlpha()
    {
        return Mathf.Abs(Mathf.Sin(Time.time - startTime));
    }

    void UpdateColor()
    {
        Color newColor = new Color(text.color.r, text.color.g, text.color.b, CalculateTextAlpha());
        text.color = newColor;
    }
	
	// Update is called once per frame
	void Update () {
        UpdateColor();
	}
}
