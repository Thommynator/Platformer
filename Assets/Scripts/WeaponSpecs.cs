using UnityEngine;

public class WeaponSpecs : MonoBehaviour
{
    public Transform attackTransform;
    public float attackRange;
    public int strength;
    public Sprite sprite;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 0, 1, 0.5f);
        Gizmos.DrawSphere(attackTransform.position, attackRange);
    }
}
