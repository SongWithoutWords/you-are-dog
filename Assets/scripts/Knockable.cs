using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CollisionDispatcher))]
public class Knockable : MonoBehaviour {

    public bool knockedDown = false;
    public Sprite[] sprites;
    public float knockdownThreshold;

	// Use this for initialization
	void Start () {
        CollisionDispatcher cd = GetComponent<CollisionDispatcher>();
        cd.OnCollisionEnter += KnockTable;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    // 
    void OnKnockedDown(Vector2 collisionDir)
    {
        knockedDown = true;

        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        rbody.rotation = -90 + Mathf.Rad2Deg * Mathf.Atan2(collisionDir.y, collisionDir.x);
        //rbody.rotation = 180;

        // Switch to knocked down sprite
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[1];   
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
