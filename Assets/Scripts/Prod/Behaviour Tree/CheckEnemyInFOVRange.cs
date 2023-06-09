using UnityEngine;

using BehaviorTree;

public class CheckEnemyInFOVRange : Node
{
    private static int enemyLayerMask = 1 << 6;
    private AudioSource audio;

    private Transform transform;
    private Animator anim;

    public CheckEnemyInFOVRange(Transform _transform)
    {
        transform = _transform;
        audio = transform.GetComponent<AudioSource>();
        anim = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t == null)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, GuardBT.fovRange, enemyLayerMask);

            if (colliders.Length > 0)
            {
                parent.parent.SetData("target", colliders[0].transform);
                audio.Play();
                anim.SetBool("Walking", true);

                state = NodeState.SUCCESS;
                return state;
            }
            state = NodeState.FAILURE;
            return state;
        }
        state = NodeState.SUCCESS;
        return state;
    }
}
