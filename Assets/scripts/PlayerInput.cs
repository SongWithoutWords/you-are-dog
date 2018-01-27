using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class PlayerInput : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        var collider = gameObject.GetComponent<CircleCollider2D>();

        var rigidBody = gameObject.GetComponent<Rigidbody2D>();

        rigidBody.AddForce(new Vector2(horizontal, vertical), ForceMode2D.Impulse);
        //collider.rigidbody2D.addforce

        print(vertical);
        print(horizontal);
    }
}
