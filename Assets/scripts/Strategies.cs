using UnityEngine;

namespace Strategies
{
    class RunForExit : IStrategy
    {
        public IStrategy Update(GameObject gameObject, AlertState alertState)
        {
            var exit = GameObject.FindGameObjectWithTag("FrontDoor");
            if (exit == null)
            {
                return this;
            }

            var move = gameObject.GetComponent<Move>();
            move.MoveTowards(exit);

            var exitPosition = exit.GetComponent<Transform>().position;
            Vector2 offsetToExit2D = (gameObject.GetComponent<Transform>().position - exitPosition);

            if (offsetToExit2D.sqrMagnitude < 0.5f)
            {
                Object.Destroy(gameObject, 0.0f);
            }
            return this;
        }
    }
}
