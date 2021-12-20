using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : Health
{
    public Transform respawnTransform;
    private UnityEvent onPlayerChangeHealth = new UnityEvent();

    void Start()
    {
        health = maxHealth;
        audioSource = GetComponent<AudioSource>();
        onPlayerChangeHealth.AddListener(GameObject.Find("Hearts").GetComponent<Hearts>().UpdateHearts);
    }

    public void RegainHealth(int amount)
    {
        health = Mathf.Min(health + amount, maxHealth);
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
        RegainHealth(maxHealth);
        transform.position = tf.position;
        transform.rotation = tf.rotation;
        gameObject.SetActive(true);
    }
}
