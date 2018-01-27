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
            if (customerAI.seat != null)
            {
                var move = gameObject.GetComponent<Move>();
                move.MoveTowards(customerAI.seat);
            }

            // Sit in seat until Escape happens.
            return alertState >= AlertState.Escape ? (new RunForExit() as IStrategy) : this;
        }
    }

    class RunForExit : IStrategy
    {
        public IStrategy Update(GameObject gameObject, AlertState alertState)
        {
            var move = gameObject.GetComponent<Move>();
            var exit = GameObject.FindGameObjectWithTag("FrontDoor");

            if (exit != null)
            {
                move.MoveTowards(exit);
            }

            var exitPosition = exit.GetComponent<Transform>().position;
            Vector2 offsetToExit2D = (gameObject.GetComponent<Transform>().position - exitPosition);

            if (offsetToExit2D.sqrMagnitude < 0.5f)
            {
                GameObject.Destroy(gameObject, 0.0f);
            }
            return this;
        }
    }
}
