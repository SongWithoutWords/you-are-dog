using UnityEngine;

[RequireComponent(typeof(Move))]
public class Jostle : MonoBehaviour
{
    public float jostleAccelerationThreshold = 1.0f;
    private Vector2 velocityPrev = Vector2.zero;
    void FixedUpdate()
    {
        var rigidBody = gameObject.GetComponent<Rigidbody2D>();
        var velocityCur = rigidBody.velocity;
        var acceleration = (velocityCur - velocityPrev) / Time.fixedDeltaTime;
        velocityPrev = velocityCur;

        var move = gameObject.GetComponent<Move>();
        var mass = rigidBody.mass;
        var intrinsicAcceleration = move.Force / mass;
        var extrinsicAcceleration = acceleration - intrinsicAcceleration;

        if (extrinsicAcceleration.magnitude > jostleAccelerationThreshold)
        {
            print(gameObject.name + " was jostled!");
        }
    }
}
