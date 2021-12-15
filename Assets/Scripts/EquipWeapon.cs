using UnityEngine;

public class EquipWeapon : MonoBehaviour
{
    public GameObject particleEffectPrefab;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            collider.GetComponent<PlayerMovement>().EquipWeapon(GetComponent<WeaponSpecs>());
            GameObject particleEffect = Instantiate(particleEffectPrefab, transform.parent);
            particleEffect.transform.SetParent(null);
            Destroy(transform.parent.gameObject);
        }
    }

}
