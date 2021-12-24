using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : Health
{
    private Vector3 respawnPosition;
    private UnityEvent onPlayerChangeHealth = new UnityEvent();

    void Awake()
    {
        respawnPosition = transform.position;
        Debug.Log("Respawn position: " + respawnPosition);
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        onPlayerChangeHealth.AddListener(GameObject.Find("Hearts").GetComponent<Hearts>().UpdateHearts);
        ResetHealth();
    }

    private void ResetHealth()
    {
        health = maxHealth;
        onPlayerChangeHealth?.Invoke();
    }

    public void RegainHealth(int amount)
    {
        health = Mathf.Clamp(health + amount, 0, maxHealth);
        onPlayerChangeHealth?.Invoke();
    }

    public override void TakeDamage(int damage)
    {
        GameObject blood = Instantiate(bloodParticleSystemPrefab, transform);
        blood.transform.SetParent(null);
        health -= damage;
        onPlayerChangeHealth?.Invoke();
        PlaySound(hitSounds);
        if (health <= 0)
        {
            Die();
            Respawn(respawnPosition);
        }
    }

    private void Respawn(Vector3 position)
    {
        ResetHealth();
        transform.position = position;
        gameObject.SetActive(true);
    }
}
