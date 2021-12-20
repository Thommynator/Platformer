using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : Health
{
    public Transform respawnTransform;
    private UnityEvent onPlayerChangeHealth = new UnityEvent();

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
        Debug.Log("Take damage new");
        GameObject blood = Instantiate(bloodParticleSystemPrefab, transform);
        blood.transform.SetParent(null);
        health -= damage;
        onPlayerChangeHealth?.Invoke();
        PlaySound(hitSounds);
        if (health <= 0)
        {
            Die();
            Respawn(respawnTransform);
        }
    }

    private void Respawn(Transform tf)
    {
        ResetHealth();
        transform.position = tf.position;
        transform.rotation = tf.rotation;
        gameObject.SetActive(true);
    }
}
