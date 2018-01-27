using UnityEngine;

public interface IStrategy
{
    IStrategy Update(GameObject gameObject, AlertState alertState);
}

public abstract class AIBase : MonoBehaviour
{
    protected IStrategy strategy;

    void FixedUpdate()
    {
        var restaurant = FindObjectOfType<RestaurantState>();
        strategy = strategy.Update(gameObject, restaurant.alertState);
    }
}
