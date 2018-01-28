using UnityEngine;

[RequireComponent(typeof(Move))]
public class CustomerAI : AIBase
{
    public GameObject seat;
    
    void Start()
    {
        strategy = new SitInSeat();
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
