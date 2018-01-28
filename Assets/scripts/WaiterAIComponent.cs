using UnityEngine;

[RequireComponent(typeof(Move))]
public class WaiterAIComponent : AIBase
{
    void Start()
    {
        strategy = new RelaxedStrategy();
    }

    // A strategy to wander around the restaurant.
    class WanderStrategy : IStrategy
    {
        private Vector2 targetPosition;
        private bool collidedWithPlayer = false;

        public WanderStrategy(GameObject gameObject)
        {
            targetPosition.x = Random.Range(-5.0f, 5.0f);
            targetPosition.y = Random.Range(-4.0f, 4.0f);

            var collisionDispatcher = gameObject.GetComponent<CollisionDispatcher>();
            collisionDispatcher.OnCollisionEnter += OnCollisionEnter;
        }

        public IStrategy Update(GameObject gameObject, AlertState alertState)
        {
            var transform = gameObject.GetComponent<Transform>();
            Vector2 position2D = transform.position;
            Vector2 offsetToTarget = targetPosition - position2D;
            float distanceSquaredToTarget = offsetToTarget.sqrMagnitude;

            // If the waiter reaches the target position, it returns control to the alert state strategy.
            if (distanceSquaredToTarget < 1.0f)
            {
                switch (alertState)
                {
                    case AlertState.Relaxed: return new RelaxedStrategy();
                    case AlertState.Alert: return new AlertStrategy();
                    case AlertState.Aware: return new AwareStrategy();
                    case AlertState.Escape: return new EscapeStrategy();
                    default: return this;
                }
            }

            // Otherwise, the waiter applies force toward the target position.
            gameObject.GetComponent<Move>().GravitateTowards(targetPosition);

            // If the restaurant is relaxed, continue wandering.
            // If the restaurant is alert, wander as long as it doesn't run into the player.
            // Otherwise return control to the state strategy.
            switch (alertState)
            {
                case AlertState.Relaxed: return this;
                case AlertState.Alert: return collidedWithPlayer ? (new AlertStrategy() as IStrategy) : this;
                case AlertState.Aware: return new AwareStrategy();
                case AlertState.Escape: return new EscapeStrategy();
                default: return this;
            }
        }

        private void OnCollisionEnter(Collision2D collision, float acceleration)
        {
            if (collision.collider.GetComponent<PlayerInput>() != null)
            {
                collidedWithPlayer = true;
            }
        }
    }

    // During the relaxed state, waiters wander around the restaurant.
    class RelaxedStrategy : IStrategy
    {
        public IStrategy Update(GameObject gameObject, AlertState alertState)
        {
            // The waiter, when relaxed, wanders.
            switch (alertState)
            {
                case AlertState.Relaxed: return new WanderStrategy(gameObject);
                case AlertState.Alert: return new AlertStrategy();
                case AlertState.Aware: return new AwareStrategy();
                case AlertState.Escape: return new EscapeStrategy();
                default: return this;
            }
        }
    }

    // During the alert state, the waiters wander. However, if the player is nearby when the waiter
    // is between target positions of wandering, the waiter will be pulled toward the player.
    class AlertStrategy : IStrategy
    {
        public IStrategy Update(GameObject gameObject, AlertState alertState)
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            var playerTransform = player.GetComponent<Transform>();

            var transform = gameObject.GetComponent<Transform>();
            Vector3 offsetToPlayer = playerTransform.position - transform.position;
            float distanceSquaredToPlayer = offsetToPlayer.sqrMagnitude;

            // Move toward the player if the player is nearby between wandering target positions.
            if (distanceSquaredToPlayer < 2.0f)
            {
                gameObject.GetComponent<Move>().GravitateTowards(player);

                switch (alertState)
                {
                    case AlertState.Relaxed: return new RelaxedStrategy();
                    case AlertState.Alert: return this;
                    case AlertState.Aware: return new AwareStrategy();
                    case AlertState.Escape: return new EscapeStrategy();
                    default: return this;
                }
            }

            // If the player isn't nearby, drop into the wander strategy.
            switch (alertState)
            {
                case AlertState.Relaxed: return new RelaxedStrategy();
                case AlertState.Alert: return new WanderStrategy(gameObject);
                case AlertState.Aware: return new AwareStrategy();
                case AlertState.Escape: return new EscapeStrategy();
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

            gameObject.GetComponent<Move>().MoveTowards(player);

            switch (alertState)
            {
                case AlertState.Relaxed: return new RelaxedStrategy();
                case AlertState.Alert: return new AlertStrategy();
                case AlertState.Aware: return this;
                case AlertState.Escape: return new EscapeStrategy();
                default: return this;
            }
        }
    }

    // TODO What do waiters do during the escape state?
    class EscapeStrategy : IStrategy
    {
        public IStrategy Update(GameObject gameObject, AlertState alertState)
        {
            switch (alertState)
            {
                case AlertState.Relaxed: return new RelaxedStrategy();
                case AlertState.Alert: return new AlertStrategy();
                case AlertState.Aware: return new AwareStrategy();
                case AlertState.Escape: return this;
                default: return this;
            }
        }
    }
}
