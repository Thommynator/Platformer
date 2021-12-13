using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SoftShake()
    {
        animator.SetTrigger("softShake");
    }
    public void MediumShake()
    {
        animator.SetTrigger("mediumShake");
    }
    public void StrongShake()
    {
        Debug.Log("Strong shake not implemented yet");
        animator.SetTrigger("strongShake");
    }

}
