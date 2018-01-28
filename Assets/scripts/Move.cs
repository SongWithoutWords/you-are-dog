using UnityEngine;

//using Extensions;

[RequireComponent(typeof(Rigidbody2D))]
public class Move : MonoBehaviour
{
    public float forcefullness = 1;

    private Vector2 movementForce = Vector2.zero;

    public void GravitateTowards(GameObject target)
    {
        GravitateTowards((Vector2)target.transform.position);
    }
    public void GravitateTowards(Vector2 target)
    {
        Vector2 position = transform.position;
        Vector2 offsetToTarget = target - position;
        float distanceSquaredToTarget = offsetToTarget.sqrMagnitude;
        AddForce(forcefullness * offsetToTarget / distanceSquaredToTarget);
    }
    public void MoveTowards(GameObject target)
    {
        MoveTowards((Vector2)target.transform.position);
    }
    public void MoveTowards(Vector2 target)
    {
        Vector2 position = transform.position;
        Vector2 offsetToTarget = target - position;
        AddForce(forcefullness * offsetToTarget.normalized);
    }
    public void AddForce(Vector2 force)
    {
        movementForce += force;
    }
    public Vector2 Force
    {
        get { return movementForce; }
    }

    void FixedUpdate()
    {
        var rigidBody = gameObject.GetComponent<Rigidbody2D>();
        rigidBody.AddForce(movementForce, ForceMode2D.Force);
        movementForce = Vector2.zero;
    }
}
