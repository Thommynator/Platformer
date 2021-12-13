using UnityEngine;

public class Health : MonoBehaviour
{

    public int maxHealth;
    private int health;

    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log(gameObject.name + " died!");
        Destroy(gameObject);
    }
}
