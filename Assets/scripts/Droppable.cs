using UnityEngine; 

public class Droppable : MonoBehaviour
{
    public float collisionDropThreshold = 1.0f;
    public GameObject droppingPrefab;

    void Start()
    {
        var cd = gameObject.transform.parent.GetComponent<CollisionDispatcher>();
        if (cd != null)
        {
            cd.OnCollisionEnter += OnCollision;
        }
    }

    void OnCollision(Collision2D collision, float acceleration)
    {
        if (acceleration > collisionDropThreshold)
        {
            OnDrop(collision);
        }
    }

    void OnDrop(Collision2D collision)
    {
        var dropping = Instantiate(droppingPrefab);
        dropping.transform.position = transform.position;
        dropping.transform.rotation = transform.rotation;
        var rigidBody = dropping.GetComponent<Rigidbody2D>();
        if (rigidBody != null)
        {
            rigidBody.velocity += collision.relativeVelocity;
        }

        var cd = gameObject.transform.parent.GetComponent<CollisionDispatcher>();
        if (cd != null)
        {
            cd.OnCollisionEnter -= OnCollision;
        }

        transform.parent = null;
        Destroy(gameObject);
    }
}
