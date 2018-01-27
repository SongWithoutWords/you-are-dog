using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class WaiterAIComponent : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        var restaurant = GameObject.FindGameObjectWithTag("Restaurant");
        var restaurantComponent = restaurant.GetComponent<RestaurantState>();
        switch (restaurantComponent.alert)
        {
            case AlertState.calm:
                UpdateRelaxed();
                break;
            case AlertState.alert:
                UpdateAlert();
                break;
            case AlertState.aware:
                UpdateAware();
                break;
            case AlertState.gtfo:
                UpdateEscape();
                break;
        }
    }

    // During the relaxed state, waiters wander around the restaurant and serve customers.
    private void UpdateRelaxed()
    {
        // TODO wander
        // TODO serve
    }

    // During the alert state, the waiters have a gravitational attraction toward the player.
    // However, their primary behaviour is still to wander.
    private void UpdateAlert()
    {
        // TODO wander
        var player = GameObject.FindGameObjectWithTag("Player");
        var playerTransform = player.GetComponent<Transform>();

        var transform = GetComponent<Transform>();
        Vector3 offsetToPlayer = transform.position - playerTransform.position;
        float distanceSquaredToPlayer = offsetToPlayer.sqrMagnitude;

        Vector2 forceToPlayer = new Vector2(offsetToPlayer.x, offsetToPlayer.y);
        forceToPlayer /= distanceSquaredToPlayer;

        var rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.AddForce(forceToPlayer);
    }

    // During the aware state, one waiter attempts to call animal control while the others bolt toward the player.
    private void UpdateAware()
    {
        // TODO phone animal control
        var player = GameObject.FindGameObjectWithTag("Player");
        var playerTransform = player.GetComponent<Transform>();

        var transform = GetComponent<Transform>();
        Vector3 offsetToPlayer = transform.position - playerTransform.position;
        float distanceToPlayer = offsetToPlayer.magnitude;

        Vector2 forceToPlayer = new Vector2(offsetToPlayer.x, offsetToPlayer.y);
        forceToPlayer /= distanceToPlayer;

        var rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.AddForce(forceToPlayer);
    }

    private void UpdateEscape()
    {
        // TODO
    }
}
