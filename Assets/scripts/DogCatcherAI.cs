using UnityEngine;

[RequireComponent(typeof(Move))]
public class DogCatcherAI : AIBase
{
    void Start()
    {
        strategy = new PursuePlayer();
    }
    
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
                case AlertState.Aware:
                    return new Strategies.RunForExit();
                case AlertState.Escape:
                case AlertState.Caught:
                    return this;
                case AlertState.GotAway:
                    return new Strategies.RunForExit();
                default:
                    return this;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<PlayerInput>() != null)
        {
            var restaurant = FindObjectOfType<RestaurantState>();
            restaurant.NotifyPlayerCaught();

            // Attach the player to the dog catcher.
            Destroy(collision.collider.GetComponent<FaceDirectionOfMotion>());
            Destroy(collision.collider.GetComponent<Rigidbody2D>());
            collision.collider.GetComponent<Transform>().SetParent(GetComponent<Transform>());

        }
    }
}
