using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Dropping : MonoBehaviour
{
    public float velocityThreshold = 1.0f;
    public GameObject droppedPrefab;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var rigidBody = gameObject.GetComponent<Rigidbody2D>();
        if (rigidBody.velocity.magnitude < velocityThreshold)
        {
            var dropped = Instantiate(droppedPrefab);
            dropped.transform.position = transform.position;
            dropped.transform.rotation = transform.rotation;

            transform.parent = null;
            Destroy(gameObject);
        }
    }
}
