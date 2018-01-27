using UnityEngine;

[RequireComponent(typeof(Move))]
public class CustomerAI : AIBase
{
    public float forcefullness;

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
                //exit.transform.
                move.MoveTowards(exit);
            }

            GameObject.Destroy(gameObject, 0.0f);
            return this;
        }
    }
}
