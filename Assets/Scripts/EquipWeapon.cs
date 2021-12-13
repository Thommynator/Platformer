using UnityEngine;

public class EquipWeapon : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            collider.GetComponent<PlayerMovement>().EquipWeapon(GetComponent<WeaponSpecs>());
            Destroy(transform.parent.gameObject);
        }
    }
}
