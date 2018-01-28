using UnityEngine;

public class CollisionDispatcher : MonoBehaviour
{
    public delegate void CollisionHandler(Collision2D collision, float acceleration);

    public event CollisionHandler OnCollisionEnter;
    public event CollisionHandler OnCollisionStay;
    public event CollisionHandler OnCollisionExit;

    float CalculateAccelerationFromCollision(Collision2D collision)
    {
        var otherBody = collision.rigidbody;
        var otherMass = otherBody.bodyType == RigidbodyType2D.Static ? float.MaxValue : otherBody.mass;

        var thisBody = gameObject.GetComponent<Rigidbody2D>();
        var thisMass = otherBody.bodyType == RigidbodyType2D.Static ? float.MaxValue : thisBody.mass;

        var deltaV = collision.relativeVelocity.magnitude * otherMass / (thisMass + otherMass);

        return deltaV / Time.fixedDeltaTime;
    }

    void FixedUpdate() {}

    void OnCollisionEnter2D(Collision2D collision)
    {
        var acceleration = CalculateAccelerationFromCollision(collision);
        if (OnCollisionEnter != null)
        {
            OnCollisionEnter(collision, acceleration);
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        var acceleration = CalculateAccelerationFromCollision(collision);
        if (OnCollisionStay != null)
        {
            OnCollisionStay(collision, acceleration);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        var acceleration = CalculateAccelerationFromCollision(collision);
        if (OnCollisionExit != null)
        {
            OnCollisionExit(collision, acceleration);
        }
    }
}
