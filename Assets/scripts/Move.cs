using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Move : MonoBehaviour
{
    private Vector2 movementForce = Vector2.zero;
    public void AddForce(Vector2 force)
    {
        movementForce += force;
    }
    public Vector2 Force
    {
        get { return movementForce; }
    }

    void Update()
    {
        var rigidBody = gameObject.GetComponent<Rigidbody2D>();
        rigidBody.AddForce(movementForce, ForceMode2D.Force);
        movementForce = Vector2.zero;
    }
}
