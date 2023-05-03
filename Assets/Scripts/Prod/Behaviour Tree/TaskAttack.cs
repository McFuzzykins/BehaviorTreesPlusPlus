using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskAttack : Node
{
    private Transform lastTarget;
    private Animator animator;
    private EnemyManager enemyManager;

    private float attackTime = 8f;
    private float attackCounter = 0f;

    public TaskAttack(Transform transform)
    {
        animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        if (target != lastTarget)
        {
            enemyManager = target.GetComponent<EnemyManager>();
            lastTarget = target;
        }

        attackCounter += Time.deltaTime;
        if (attackCounter >= attackTime)
        {
            bool enemyIsDead = enemyManager.TakeHit();
            
            if (enemyIsDead)
            {
                ClearData("target");
                animator.SetBool("Attacking", false);
                animator.SetBool("Walking", true);
            }
            else
            {
                attackCounter = 0f;
            }
        }
        state = NodeState.RUNNING;
        return state;
    }
}
