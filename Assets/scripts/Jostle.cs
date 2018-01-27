using UnityEngine;

public class Jostle : MonoBehaviour
{
    public delegate void JostleEventHandler(Jostle jostled);
    public event JostleEventHandler RaiseJostleEvent;

    public float accelerationThreshold = 1.0f;
    public int alertAmount = 0;

    private Vector2 velocityPrev = Vector2.zero;

    private enum PlayerCollisionState
    {
        None,
        Enter,
        Stay
    }
    private PlayerCollisionState playerCollisionState = PlayerCollisionState.None;

    void FixedUpdate()
    {
        var rigidBody = gameObject.GetComponent<Rigidbody2D>();
        var velocityCur = rigidBody.velocity;
        var acceleration = (velocityCur - velocityPrev) / Time.fixedDeltaTime;
        velocityPrev = velocityCur;

        var move = gameObject.GetComponent<Move>();
        var mass = rigidBody.mass;
        var intrinsicAcceleration = move != null ? (move.Force / mass) : new Vector2(0, 0);
        var extrinsicAcceleration = acceleration - intrinsicAcceleration;

        if (playerCollisionState == PlayerCollisionState.Enter
            && extrinsicAcceleration.magnitude > accelerationThreshold)
        {
            if (RaiseJostleEvent != null)
            {
                RaiseJostleEvent(this);
            }
            playerCollisionState = PlayerCollisionState.Stay;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerInput>() != null)
        {
            playerCollisionState = PlayerCollisionState.Enter;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerInput>() != null)
        {
            playerCollisionState = PlayerCollisionState.None;
        }
    }
}
