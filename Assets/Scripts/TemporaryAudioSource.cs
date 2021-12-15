using UnityEngine;

public class TemporaryAudioSource : MonoBehaviour
{
    public void Play(AudioClip clip, float destroyAfter)
    {
        transform.SetParent(null);
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();
        Destroy(gameObject, destroyAfter);
    }

}
