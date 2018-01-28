using UnityEngine;

[RequireComponent(typeof(CollisionDispatcher), typeof(AudioSource))]
public class Knockable : MonoBehaviour {

    public bool knockedDown = false;
    public float knockdownThreshold;
    public GameObject knockedPrefab;
    public float velocityCoeff = 1.0f;
    public AudioClip knockdownSound;

	// Use this for initialization
	void Start () {
        CollisionDispatcher cd = GetComponent<CollisionDispatcher>();
        cd.OnCollisionEnter += KnockTable;
	}

    void OnKnockedDown(Vector2 collisionDir)
    {
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            if (knockdownSound != null && audioSource != null)
            {
                audioSource.enabled = true;
                audioSource.PlayOneShot(knockdownSound);
            }
        }

        knockedDown = true;

        Transform transform = GetComponent<Transform>();

        GameObject wreck = Instantiate(knockedPrefab);
        float wreckX = transform.position.x;
        float wreckY = transform.position.y;
        float wreckZ = wreck.transform.position.z;
        wreck.transform.position = new Vector3(wreckX, wreckY, wreckZ);
        wreck.transform.rotation = transform.rotation;

        Rigidbody2D newRbody = wreck.GetComponent<Rigidbody2D>();
        newRbody.velocity = collisionDir * velocityCoeff;
        newRbody.rotation = -90 + Mathf.Rad2Deg * Mathf.Atan2(collisionDir.y, collisionDir.x);
        Destroy(gameObject);
    }

    // Knock down table if collision is big enough
    void KnockTable(Collision2D c, float acceleration)
    {
        if (!knockedDown && acceleration >= knockdownThreshold)
        {
            OnKnockedDown(c.relativeVelocity);
        }
    }
}
