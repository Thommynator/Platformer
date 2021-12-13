using UnityEngine;

public class FlipBasedOnVelocity : MonoBehaviour
{

    Rigidbody2D body;
    public bool invertDirection;

    void Start()
    {
        TryGetComponent<Rigidbody2D>(out body);
        if (body == null)
        {
            Debug.LogError("Rigidbody2D component missing on " + gameObject.name + "!");
            return;
        }
    }

    void Update()
    {
        float threshold = 0.1f;


        int inversionFactor = invertDirection ? -1 : 1;
        if (body.velocity.x > threshold)
        {
            transform.localScale = new Vector3(inversionFactor * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (body.velocity.x < threshold)
        {
            transform.localScale = new Vector3(inversionFactor * -Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

    }

}
