using UnityEngine;

[RequireComponent(typeof(Move))]
public class WaiterAIComponent : AIBase
{
    // During the relaxed state, waiters wander around the restaurant and serve customers.
    protected override void UpdateRelaxed()
    {
        // TODO wander
        // TODO serve
    }

    // During the alert state, the waiters have a gravitational attraction toward the player.
    // However, their primary behaviour is still to wander.
    protected override void UpdateAlert()
    {
        // TODO wander
        var player = GameObject.FindGameObjectWithTag("Player");
        var playerTransform = player.GetComponent<Transform>();

        var transform = GetComponent<Transform>();
        Vector3 offsetToPlayer = playerTransform.position - transform.position;
        float distanceSquaredToPlayer = offsetToPlayer.sqrMagnitude;

        Vector2 forceToPlayer = new Vector2(offsetToPlayer.x, offsetToPlayer.y);
        forceToPlayer /= distanceSquaredToPlayer;

        var move = GetComponent<Move>();
        move.AddForce(forceToPlayer);
    }

    // During the aware state, one waiter attempts to call animal control while the others bolt toward the player.
    protected override void UpdateAware()
    {
        // TODO phone animal control
        var player = GameObject.FindGameObjectWithTag("Player");
        var playerTransform = player.GetComponent<Transform>();

        var transform = GetComponent<Transform>();
        Vector3 offsetToPlayer = playerTransform.position - transform.position;
        float distanceToPlayer = offsetToPlayer.magnitude;

        Vector2 forceToPlayer = new Vector2(offsetToPlayer.x, offsetToPlayer.y);
        forceToPlayer /= distanceToPlayer;

        var move = GetComponent<Move>();
        move.AddForce(forceToPlayer);
    }

    protected override void UpdateEscape()
    {
        // TODO
    }
}
