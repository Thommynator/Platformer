using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    public Sprite healthyHeart;
    public Sprite brokenHeart;
    private Image image;
    private Animator animator;

    void Awake()
    {
        image = GetComponentInChildren<Image>();
        animator = GetComponent<Animator>();
        SetToHealthyHeart();
    }

    public void SetToBrokenHeart()
    {
        animator.SetBool("isHealthy", false);
        image.sprite = brokenHeart;
    }

    public void SetToHealthyHeart()
    {
        animator.SetBool("isHealthy", true);
        image.sprite = healthyHeart;
    }
}
