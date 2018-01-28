using UnityEngine;
using UnityEngine.UI;

public class YouAreDogText : MonoBehaviour
{
    public Text text;
    public float startTime;

	void Start()
    {
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
	
	void Update()
    {
        UpdateColor();
	}
}
