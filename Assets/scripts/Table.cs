using UnityEngine;
using System.Collections;

public class Table : MonoBehaviour {

    public bool knockedDown = false;
    // When knocked down, this is the angle the table is facing
    public Vector2 facingDirRad;

    public Sprite[] sprites;

	// Use this for initialization
	void Start () {
	    
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
    void OnCollisionEnter2D(Collision2D c)
    {
        if (!knockedDown)
        {
            OnKnockedDown(c.relativeVelocity);
        }
    }
}
