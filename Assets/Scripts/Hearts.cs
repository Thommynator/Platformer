using UnityEngine;
using System.Collections.Generic;

public class Hearts : MonoBehaviour
{

    public GameObject heartPrefab;
    public Transform transformOfFirstLeftHeart;
    private int numberOfHearts;
    private List<GameObject> hearts;
    private GameObject player;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        numberOfHearts = player.GetComponent<Health>().maxHealth;
        hearts = new List<GameObject>();
        for (int i = 0; i < numberOfHearts; i++)
        {
            float spriteWidth = 200;
            Vector3 position = transformOfFirstLeftHeart.position + i * Vector3.right * spriteWidth;
            GameObject heart = Instantiate(heartPrefab, position, Quaternion.identity);
            heart.transform.SetParent(this.transform);
            hearts.Add(heart);
        }
    }

    public void UpdateHearts()
    {
        int healthyHearts = player.GetComponent<Health>().GetHealth();
        for (int i = 0; i < numberOfHearts; i++)
        {
            if (i < healthyHearts)
            {
                hearts[i].GetComponent<Heart>().SetToHealthyHeart();
            }
            else
            {
                hearts[i].GetComponent<Heart>().SetToBrokenHeart();
            }
        }
    }

}
