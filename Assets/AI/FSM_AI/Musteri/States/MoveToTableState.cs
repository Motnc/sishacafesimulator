using UnityEngine;
using BeykozEdu.FSM;

public class MoveToTableState : BaseState<CustomerStateData>
{
    public override void OnEnter()
    {
        StateData.Agent.SetDestination(StateData.TableTarget.position);
        StateData.Animator.SetBool("isWalking", true);
    }

    public override void OnUpdate()
    {
        if (!StateData.Agent.pathPending && StateData.Agent.remainingDistance < 1f)
        {
            StateData.Animator.SetBool("isWalking", false);
            StateMachineHandler.AddState(new SitToTableState(), StateData);
        }
    }

    public override void OnExit() { }
}