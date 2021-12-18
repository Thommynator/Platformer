using UnityEngine;

public class CollectableHeart : MonoBehaviour
{
    public int recoverAmount;
    public AudioClip collectSound;
    public GameObject tempAudioSource;

    void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.tag == "Player")
        {
            Instantiate(tempAudioSource, transform).GetComponent<TemporaryAudioSource>().Play(collectSound, 2);
            collider.GetComponent<Health>().RegainHealth(recoverAmount);
            Destroy(gameObject);
        }
    }
}
