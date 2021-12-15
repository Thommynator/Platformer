using UnityEngine;

public class Wizard : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform spawnTransform;

    public void Shoot()
    {
        GameObject fireball = Instantiate(fireballPrefab, spawnTransform);
        fireball.transform.SetParent(null);
    }
}
