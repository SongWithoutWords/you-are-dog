using UnityEngine;

public class ChefAI : AIBase
{
    void Start()
    {
        strategy = new ReviveRevivables();
    }

    // When the chef is not aware of the player, he walks toward revivable objects and revives them.
    class ReviveRevivables : IStrategy
    {
        public IStrategy Update(GameObject gameObject, AlertState alertState)
        {
            Revivable[] revivables = FindObjectsOfType<Revivable>();
            
            if (revivables != null && revivables.Length > 0)
            {
                Vector2 position = gameObject.GetComponent<Transform>().position;

                Revivable closest = null;
                float minDistanceSquared = float.MaxValue;
                foreach (var revivable in revivables)
                {
                    Vector2 revivablePos = revivable.GetComponent<Transform>().position;
                    float distanceSquared = (position - revivablePos).sqrMagnitude;
                    if (distanceSquared < minDistanceSquared)
                    {
                        closest = revivable;
                        minDistanceSquared = distanceSquared;
                    }
                }

                // If next to a waiter, wake them up.
                if (minDistanceSquared < 2.0f)
                {
                    closest.Revive();
                }
                // Otherwise, walk toward the nearest waiter.
                else
                {
                    gameObject.GetComponent<Move>().MoveTowards(closest.gameObject);
                }
            }

            switch (alertState)
            {
                case AlertState.Relaxed:
                case AlertState.Alert:
                case AlertState.Caught:
                case AlertState.GotAway:
                    return this;
                case AlertState.Aware:
                case AlertState.Escape:
                    return new PursuePlayer();
                default: return this;
            }
        }
    }

    // When the chef is aware of the player, he pursues the player.
    class PursuePlayer : IStrategy
    {
        public IStrategy Update(GameObject gameObject, AlertState alertState)
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            gameObject.GetComponent<Move>().MoveTowards(player);

            switch (alertState)
            {
                case AlertState.Relaxed:
                case AlertState.Alert:
                case AlertState.Caught:
                case AlertState.GotAway:
                    return new ReviveRevivables();
                case AlertState.Aware:
                case AlertState.Escape:
                    return this;
                default: return this;
            }
        }
    }
}
