using UnityEngine;

[RequireComponent(typeof(CollisionDispatcher))]
public class AlertRaiser : MonoBehaviour
{
    public float collisionAccelerationThreshold = 1;
    public float alertLevelRaised = 0;

    void Start()
    {
        var cd = gameObject.GetComponent<CollisionDispatcher>();
        if (cd != null)
        {
            cd.OnCollisionEnter += OnCollision;
        }
    }

    void OnCollision(Collision2D collision, float acceleration)
    {
        if (acceleration > collisionAccelerationThreshold
            && collision.gameObject.GetComponent<PlayerInput>() != null)
        {
            GameObject.FindObjectOfType<RestaurantState>().alertLevel += alertLevelRaised;
        }
    }
}
