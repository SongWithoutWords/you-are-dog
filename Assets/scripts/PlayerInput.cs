using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerInput : MonoBehaviour
{
    void Start() {}

    void Update()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        var forceVector = new Vector2(horizontal, vertical);

        var rigidBody = gameObject.GetComponent<Rigidbody2D>();

        rigidBody.AddForce(forceVector, ForceMode2D.Impulse);

        print(vertical);
        print(horizontal);
    }
}
