using UnityEngine;

public class Health : MonoBehaviour
{

    public GameObject bloodParticleSystemPrefab;
    public int maxHealth;
    private int health;

    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        GameObject blood = Instantiate(bloodParticleSystemPrefab, transform);
        blood.transform.SetParent(null);
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log(gameObject.name + " died!");
        FindObjectOfType<CameraShake>().MediumShake();
        Destroy(gameObject);
    }
}
