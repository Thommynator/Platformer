using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform spawnTransform;

    void Start()
    {

    }

    void Update()
    {

    }


    public void Shoot()
    {
        GameObject fireball = Instantiate(fireballPrefab, spawnTransform);
        fireball.transform.SetParent(null);
    }
}
