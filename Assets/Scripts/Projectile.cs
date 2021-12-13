using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float maxLifetime;
    private Rigidbody2D body;
    void Start()
    {
        Destroy(gameObject, maxLifetime);
    }

    void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed);
    }
}
