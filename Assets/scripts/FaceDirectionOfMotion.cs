using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FaceDirectionOfMotion : MonoBehaviour
{
    public float strength = 1.0f;
    void FixedUpdate()
    {
        var rigidBody = gameObject.GetComponent<Rigidbody2D>();

        var velocity = rigidBody.velocity;
        var speed = velocity.magnitude;

        var facingAngle = rigidBody.rotation;
        var motionAngle = Vector2.SignedAngle(Vector2.up, velocity);

        var angularDeviationRaw = (motionAngle - facingAngle) % 360;
        var angularDeviation = angularDeviationRaw < 180 ? angularDeviationRaw : angularDeviationRaw - 360;

        rigidBody.rotation += strength * Time.fixedDeltaTime * speed * angularDeviation;
    }
}
