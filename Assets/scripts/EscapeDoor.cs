using UnityEngine;

public class EscapeDoor : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerInput>() == null)
        {
            return;
        }

        var restaurant = FindObjectOfType<RestaurantState>();
        restaurant.NotifyPlayerGotAway();
    }
}

