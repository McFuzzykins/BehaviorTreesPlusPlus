using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckEnemyInAttackRange : Node
{
    private static int enemyLayerMask = 1 << 6;

    private UnityEngine.Transform transform;

    public CheckEnemyInAttackRange(UnityEngine.Transform _transform)
    {
        transform = _transform;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t == null)
        {
            state = NodeState.FAILURE;
            return state;
        }
        Transform target = (Transform)t;
        if (Vector3.Distance(transform.position, target.position) <= GuardBT.attackRange)
        {
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }
}
