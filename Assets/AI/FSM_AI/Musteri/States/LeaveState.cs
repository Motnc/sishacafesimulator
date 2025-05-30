using UnityEngine;
using BeykozEdu.FSM;

public class LeaveState : BaseState<CustomerStateData>
{
    public override void OnEnter()
    {
        StateData.Animator.SetBool("isSitting", false);
        StateData.Agent.SetDestination(StateData.ExitTarget.position);
        StateData.Animator.SetBool("isWalking", true);
    }

    public override void OnUpdate()
    {
        if (!StateData.Agent.pathPending && StateData.Agent.remainingDistance < 0.2f)
        {
            GameObject.Destroy(StateData.Agent.gameObject);
        }
    }

    public override void OnExit() { }
}

