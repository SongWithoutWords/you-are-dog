using UnityEngine;

public class Revivable : MonoBehaviour
{
    public GameObject reviveAs;

    public void Revive()
    {
        GameObject revived = Instantiate(reviveAs);
        var revivedTransform = revived.GetComponent<Transform>();

        var transform = GetComponent<Transform>();
        float revivedX = transform.position.x;
        float revivedY = transform.position.y;
        float revivedZ = revivedTransform.position.z;

        revivedTransform.position = new Vector3(revivedX, revivedY, revivedZ);
        revivedTransform.rotation = transform.rotation;

        Destroy(gameObject);
    }
}
