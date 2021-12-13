using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damage;
    public LayerMask receivesDamage;
    public bool shouldDestroyItselfOnCollision;

    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (IsInLayerMask(collider.gameObject, receivesDamage))
        {
            Debug.Log("Take Damage");
            collider.GetComponent<Health>().TakeDamage(damage);

            if (shouldDestroyItselfOnCollision)
            {
                Destroy(gameObject);
            }
        }
    }

    private bool IsInLayerMask(GameObject @object, LayerMask layerMask)
    {
        return ((layerMask.value & (1 << @object.layer)) > 0);
    }

}
