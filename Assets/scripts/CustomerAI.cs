using UnityEngine;

[RequireComponent(typeof(Move))]
public class CustomerAI : AIBase
{
    // Use this for initialization
    void Start()
    {
        strategy = new SitStill();
    }

    class SitStill : IStrategy
    {
        // Sit still and do nothing, until pandemonium breaks out
        public IStrategy Update(GameObject gameObject, AlertState alertState)
        {
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

            if (offsetToExit2D.sqrMagnitude < 1.0f)
            {
                GameObject.Destroy(gameObject, 0.0f);
            }
            return this;
        }
    }
}
