using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskPatrol : Node
{
    private Transform transform;
    private Transform[] waypoints;
    private Animator animator;

    private int currentWaypointIndex = 0;

    private float waitTime = 1f;
    private float waitCounter = 0f;
    private bool isWaiting = false;

    public TaskPatrol(Transform _transform, Transform[] _waypoints)
    {
        transform = _transform;
        waypoints = _waypoints;
        animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        if (isWaiting)
        {
            waitCounter += Time.deltaTime;
            if (waitCounter >= waitTime)
            {
                isWaiting = false;
                animator.SetBool("Walking", true);
            }
        }
        else
        {
            Transform wp = waypoints[currentWaypointIndex];
            if (Vector3.Distance(transform.position, wp.position) < 0.01f)
            {
                transform.position = wp.position;
                waitCounter = 0f;
                isWaiting = true;


                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
                animator.SetBool("Walking", false);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, wp.position, GuardBT.speed * Time.deltaTime);
                transform.LookAt(wp.position);
            }
        }
        state = NodeState.RUNNING;
        return state;
    }
}
