using UnityEngine;

public class Health : MonoBehaviour
{

    public GameObject bloodParticleSystemPrefab;
    public int maxHealth;
    private int health;

    [Header("Sounds")]
    private AudioSource audioSource;
    public GameObject temporaryAudioSourcePrefab;
    public AudioClip splatterSound;
    public AudioClip[] hitSounds;

    void Start()
    {
        health = maxHealth;
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
    {
        GameObject blood = Instantiate(bloodParticleSystemPrefab, transform);
        blood.transform.SetParent(null);
        health -= damage;
        PlaySound(hitSounds);
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log(gameObject.name + " died!");
        FindObjectOfType<CameraShake>().MediumShake();
        Instantiate(temporaryAudioSourcePrefab, transform).GetComponent<TemporaryAudioSource>().Play(splatterSound, 5);
        Destroy(gameObject);
    }

    private void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    private void PlaySound(AudioClip[] clips)
    {
        PlaySound(clips[Random.Range(0, clips.Length - 1)]);
    }

}
