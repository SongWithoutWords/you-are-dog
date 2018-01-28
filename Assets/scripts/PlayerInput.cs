using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Move))]
public class PlayerInput : MonoBehaviour
{
    public float staminaRegenRate = 0.2f;
    public float forceMultiplierWhileUsingStamina = 1.5f;
    public float maxStaminaSeconds = 5.0f;
    public Text staminaDisplayText;

    private float staminaSeconds;

    private void Awake()
    {
        staminaSeconds = maxStaminaSeconds;
    }

    float GetForceMultiplierAndUpdateStamina()
    {
        if (Input.GetKey(KeyCode.LeftShift) && staminaSeconds > Time.fixedDeltaTime)
        {
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
