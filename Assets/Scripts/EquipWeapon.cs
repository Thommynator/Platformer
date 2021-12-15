using UnityEngine;

public class EquipWeapon : MonoBehaviour
{
    public GameObject particleEffectPrefab;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            collider.GetComponent<Player>().EquipWeapon(GetComponent<WeaponSpecs>());
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<AudioSource>().Play();
            Instantiate(particleEffectPrefab, transform.parent);
            Destroy(transform.parent.gameObject, 5);
        }
    }

}
