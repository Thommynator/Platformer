using UnityEngine;

public class Goal : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            GameObject.Find("LevelManager").GetComponent<LevelManager>().LoadNextScene();
        }
    }

}
