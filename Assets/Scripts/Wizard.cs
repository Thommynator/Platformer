using UnityEngine;

public class Wizard : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform spawnTransform;

    public void Shoot()
    {
        GameObject fireball = Instantiate(fireballPrefab, spawnTransform.position, spawnTransform.localRotation);
        fireball.transform.SetParent(null);
    }
}
