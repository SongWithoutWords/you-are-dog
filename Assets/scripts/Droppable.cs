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
            Drop(collision);
        }
    }

    public void Drop(Collision2D collision)
    {
        Transform transform = GetComponent<Transform>();

        GameObject dropping = Instantiate(droppingPrefab);
        float droppingX = transform.position.x;
        float droppingY = transform.position.y;
        float droppingZ = dropping.transform.position.z;
        dropping.transform.position = new Vector3(droppingX, droppingY, droppingZ);
        dropping.transform.rotation = transform.rotation;

        var rigidBody = dropping.GetComponent<Rigidbody2D>();
        if (rigidBody != null && collision != null)
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
