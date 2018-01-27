using UnityEngine;

[RequireComponent(typeof(Move))]
public class WaiterAIComponent : AIBase
{
    // During the relaxed state, waiters wander around the restaurant and serve customers.
    class RelaxedStrategy : IStrategy
    {
        public IStrategy Update(GameObject gameObject, AlertState alertState)
        {
            // TODO wander
            // TODO serve

            switch (alertState)
            {
                case AlertState.calm: return this;
                case AlertState.alert: return new AlertStrategy();
                case AlertState.aware: return new AwareStrategy();
                case AlertState.gtfo: return new EscapeStrategy();
                default: return this;
            }
        }
    }

    // During the alert state, the waiters have a gravitational attraction toward the player.
    // However, their primary behaviour is still to wander.
    class AlertStrategy : IStrategy
    {
        public IStrategy Update(GameObject gameObject, AlertState alertState)
        {
            // TODO wander
            var player = GameObject.FindGameObjectWithTag("Player");
            var playerTransform = player.GetComponent<Transform>();

            var transform = gameObject.GetComponent<Transform>();
            Vector3 offsetToPlayer = playerTransform.position - transform.position;
            float distanceSquaredToPlayer = offsetToPlayer.sqrMagnitude;

            Vector2 forceToPlayer = new Vector2(offsetToPlayer.x, offsetToPlayer.y);
            forceToPlayer /= distanceSquaredToPlayer;

            var move = gameObject.GetComponent<Move>();
            move.AddForce(forceToPlayer);

            switch (alertState)
            {
                case AlertState.calm: return this;
                case AlertState.alert: return new AlertStrategy();
                case AlertState.aware: return new AwareStrategy();
                case AlertState.gtfo: return new EscapeStrategy();
                default: return this;
            }
        }
    }

    // During the aware state, one waiter attempts to call animal control while the others bolt toward the player.
    class AwareStrategy : IStrategy
    {
        public IStrategy Update(GameObject gameObject, AlertState alertState)
        {
            // TODO phone animal control
            var player = GameObject.FindGameObjectWithTag("Player");
            var playerTransform = player.GetComponent<Transform>();

            var transform = gameObject.GetComponent<Transform>();
            Vector3 offsetToPlayer = playerTransform.position - transform.position;
            float distanceToPlayer = offsetToPlayer.magnitude;

            Vector2 forceToPlayer = new Vector2(offsetToPlayer.x, offsetToPlayer.y);
            forceToPlayer /= distanceToPlayer;

            var move = gameObject.GetComponent<Move>();
            move.AddForce(forceToPlayer);

            switch (alertState)
            {
                case AlertState.calm: return this;
                case AlertState.alert: return new AlertStrategy();
                case AlertState.aware: return new AwareStrategy();
                case AlertState.gtfo: return new EscapeStrategy();
                default: return this;
            }
        }
    }

    class EscapeStrategy : IStrategy
    {
        public IStrategy Update(GameObject gameObject, AlertState alertState)
        {
            // TODO
            switch (alertState)
            {
                case AlertState.calm: return this;
                case AlertState.alert: return new AlertStrategy();
                case AlertState.aware: return new AwareStrategy();
                case AlertState.gtfo: return new EscapeStrategy();
                default: return this;
            }
        }
    }

    void Start()
    {
        strategy = new RelaxedStrategy();
    }
}
