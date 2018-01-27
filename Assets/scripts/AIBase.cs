using UnityEngine;

public abstract class AIBase : MonoBehaviour
{
    void Update()
    {
        var restaurant = GameObject.FindGameObjectWithTag("Restaurant");
        var restaurantComponent = restaurant.GetComponent<RestaurantState>();
        switch (restaurantComponent.alert)
        {
            case AlertState.calm:
                UpdateRelaxed();
                break;
            case AlertState.alert:
                UpdateAlert();
                break;
            case AlertState.aware:
                UpdateAware();
                break;
            case AlertState.gtfo:
                UpdateEscape();
                break;
        }
    }

    protected abstract void UpdateRelaxed();
    protected abstract void UpdateAlert();
    protected abstract void UpdateAware();
    protected abstract void UpdateEscape();
}
