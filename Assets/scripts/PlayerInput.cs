using UnityEngine;

//using static MyExtensions;

[RequireComponent(typeof(Move))]
public class PlayerInput : MonoBehaviour
{
    void Update()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        var forceVector = new Vector2(horizontal, vertical);

        var move = gameObject.GetComponent<Move>();

        move.AddForce(move.forcefullness * forceVector);
    }
}
