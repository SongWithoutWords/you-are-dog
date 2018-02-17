using UnityEngine;

public interface IStrategy
{
    IStrategy Update(GameObject gameObject, AlertState alertState);
}

public abstract class AIBase : MonoBehaviour
{
    protected IStrategy strategy;
    protected RestaurantState restaurant;
    
    void FixedUpdate()
    {
        strategy = strategy.Update(gameObject, restaurant.alertState);
    }
}
