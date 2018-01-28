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

            return this;
        }
    }
}
