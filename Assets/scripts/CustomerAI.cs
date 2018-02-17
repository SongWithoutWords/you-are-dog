using UnityEngine;

[RequireComponent(typeof(Move))]
public class CustomerAI : AIBase
{
    public GameObject seat;
    
    void Start()
    {
        strategy = new SitInSeat();
        restaurant = FindObjectOfType<RestaurantState>();

        if (seat == null)
        {
            Vector2 position = GetComponent<Transform>().position;
            GameObject[] seats = GameObject.FindGameObjectsWithTag("Seat");

            GameObject closest = null;
            float minDistanceSquared = float.MaxValue;
            foreach (var candidate in seats)
            {
                Vector2 candidatePos = candidate.GetComponent<Transform>().position;
                float distanceSquared = (position - candidatePos).sqrMagnitude;
                if (distanceSquared < minDistanceSquared)
                {
                    closest = candidate;
                    minDistanceSquared = distanceSquared;
                }
            }

            seat = closest;
        }
    }

    class SitInSeat : IStrategy
    {
        public IStrategy Update(GameObject gameObject, AlertState alertState)
        {
            var customerAI = gameObject.GetComponent<CustomerAI>();

            // If the customer has a seat, it in it until Escape happens.
            if (customerAI.seat != null)
            {
                var move = gameObject.GetComponent<Move>();
                move.MoveTowards(customerAI.seat);

                return alertState == AlertState.Escape ? (new Strategies.RunForExit() as IStrategy) : this;
            }

            // If the customer doesn't have a seat, it just leaves the restaurant.
            return new Strategies.RunForExit();
        }
    }
}
