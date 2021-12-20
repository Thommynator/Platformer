using UnityEngine;

public class Health : MonoBehaviour
{

    public GameObject bloodParticleSystemPrefab;
    public int maxHealth;
    protected int health;

    [Header("Sounds")]
    protected AudioSource audioSource;
    public GameObject temporaryAudioSourcePrefab;
    public AudioClip splatterSound;
    public AudioClip[] hitSounds;

    void Start()
    {
        health = maxHealth;
        audioSource = GetComponent<AudioSource>();
    }

    public virtual void TakeDamage(int damage)
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

    public int GetHealth()
    {
        return health;
    }

    public void Die()
    {
        Debug.Log(gameObject.name + " died!");
        FindObjectOfType<CameraShake>().MediumShake();
        Instantiate(temporaryAudioSourcePrefab, transform).GetComponent<TemporaryAudioSource>().Play(splatterSound, 5);
        gameObject.SetActive(false);
    }

    protected void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    protected void PlaySound(AudioClip[] clips)
    {
        PlaySound(clips[Random.Range(0, clips.Length - 1)]);
    }

}
