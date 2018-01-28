using UnityEngine;

[RequireComponent(typeof(CollisionDispatcher))]
public class AlertRaiser : MonoBehaviour
{
    public float collisionAccelerationThreshold = 1;
    public float alertLevelRaised = 0;

    void OnCollision(Collision2D collision, float acceleration)
    {
        if (acceleration > collisionAccelerationThreshold)
        {
            GameObject.FindObjectOfType<RestaurantState>().alertLevel += alertLevelRaised;
        }
    }
}
