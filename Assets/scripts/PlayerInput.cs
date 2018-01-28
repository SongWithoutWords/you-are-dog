using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Move), typeof(SpriteRenderer))]
public class PlayerInput : MonoBehaviour
{
    public float staminaRegenRate = 0.2f;
    public float forceMultiplierWhileUsingStamina = 1.5f;
    public float maxStaminaSeconds = 5.0f;
    public Text staminaDisplayText;
    public Sprite capturedSprite;
    public List<Sprite> sprites;
    public float animationDuration = 1.0f;
    public float firstBoostAlertAmount = 20.0f;

    [HideInInspector]
    public bool isCaptured = false;

    private float staminaSeconds = 0.0f;
    private bool hasUsedBoost = false;

    private float timeSinceFirstBoost = 0;
    private SpriteRenderer spriteRenderer = null;

    private void Awake()
    {
        staminaSeconds = maxStaminaSeconds;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    float GetForceMultiplierAndUpdateStamina()
    {
        if (Input.GetKey(KeyCode.LeftShift) && staminaSeconds > Time.fixedDeltaTime)
        {
            if (!hasUsedBoost)
            {
                var restaurant = Object.FindObjectOfType<RestaurantState>();
                if (restaurant != null)
                {
                    restaurant.AddAlert(firstBoostAlertAmount);
                }
                hasUsedBoost = true;
            }
            staminaSeconds -= Time.fixedDeltaTime;
            return forceMultiplierWhileUsingStamina;
        }
        else
        {
            staminaSeconds += staminaRegenRate * Time.fixedDeltaTime;
            staminaSeconds = Mathf.Min(staminaSeconds, maxStaminaSeconds);
            return 1;
        }
    }

    void Update()
    {
        if (staminaDisplayText != null)
        {
            staminaDisplayText.text = "Stamina: " + staminaSeconds.ToString("0.0") + "/" + maxStaminaSeconds.ToString("0.0");
        }
        if (isCaptured)
        {
            spriteRenderer.sprite = capturedSprite;
        }
        else if (hasUsedBoost)
        {
            timeSinceFirstBoost += Time.deltaTime;
            var numSprites = sprites.Count;
            var spriteIndex = Mathf.Clamp(Mathf.RoundToInt(numSprites * timeSinceFirstBoost / animationDuration), 0, numSprites - 1);
            spriteRenderer.sprite = sprites[spriteIndex];
        }
    }

    void FixedUpdate()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        var forceVector = new Vector2(horizontal, vertical);

        var forceBoost = GetForceMultiplierAndUpdateStamina();
        var move = gameObject.GetComponent<Move>();

        move.AddForce(forceBoost * move.forcefullness * forceVector);
    }
}
