using UnityEngine;

public class Bat : MonoBehaviour
{

    public float attackRange;
    public GameObject projectilePrefab;
    private GameObject player;
    private Animator animator;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < attackRange)
        {
            animator.SetTrigger("attack");
        }
    }

    private void StartAttackAnimation()
    {

    }
    public void Attack()
    {
        Debug.Log("Attack");
        Vector3 directionToPlayer = player.transform.position - transform.position;
        float angle = Vector3.Angle(Vector3.right, directionToPlayer);
        if (player.transform.position.y < transform.position.y) angle *= -1;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Instantiate(projectilePrefab, transform.position, rotation);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
