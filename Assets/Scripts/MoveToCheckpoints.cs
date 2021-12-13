using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToCheckpoints : MonoBehaviour
{

    public List<Transform> checkpoints;
    public float speed;
    public float waitSecondsOnTurn;
    private float remainingWaitTime;
    private int currentCheckpointIndex;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        currentCheckpointIndex = 0;
        remainingWaitTime = waitSecondsOnTurn;
        transform.position = checkpoints[currentCheckpointIndex].position;

    }

    void FixedUpdate()
    {
        if (checkpoints.Count > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, checkpoints[currentCheckpointIndex].position, speed);
            transform.rotation = checkpoints[currentCheckpointIndex].rotation;

            if (Vector2.Distance(transform.position, checkpoints[currentCheckpointIndex].position) < 2f)
            {
                animator.SetBool("isRunning", false);
                if (remainingWaitTime <= 0)
                {
                    // go to next checpoint
                    currentCheckpointIndex = currentCheckpointIndex == checkpoints.Count - 1 ? 0 : currentCheckpointIndex + 1;
                    remainingWaitTime = waitSecondsOnTurn;
                    animator.SetBool("isRunning", true);
                }
                else
                {
                    remainingWaitTime -= Time.deltaTime;
                }
            }
        }
    }
}
