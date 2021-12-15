using UnityEngine;

public class TranslateXLoop : MonoBehaviour
{

    public Transform leftEnd;
    public Transform rightEnd;
    public float minRandomSpeed;
    public float maxRandomSpeed;
    private float speed;
    public bool leftToRight;


    void Start()
    {
        speed = Random.Range(minRandomSpeed, maxRandomSpeed);
    }

    void Update()
    {
        int direction = leftToRight ? 1 : -1;
        transform.position = new Vector3(transform.position.x + direction * speed * Time.deltaTime, transform.position.y, transform.position.z);
        if (transform.position.x > rightEnd.position.x)
        {
            transform.position = new Vector3(leftEnd.position.x, transform.position.y, transform.position.z);
        }
        if (transform.position.x < leftEnd.position.x)
        {
            transform.position = new Vector3(rightEnd.position.x, transform.position.y, transform.position.z);
        }
    }
}
