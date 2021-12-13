using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float turnSpeed;

    void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * turnSpeed);
    }
}
