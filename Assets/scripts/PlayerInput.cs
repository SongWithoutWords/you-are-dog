using UnityEngine;

[RequireComponent(typeof(Move))]
public class PlayerInput : MonoBehaviour
{
    public float movementForce = 1;
    void Update()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        var forceVector = new Vector2(horizontal, vertical);

        var move = gameObject.GetComponent<Move>();

        move.AddForce(movementForce * forceVector);
    }
}
