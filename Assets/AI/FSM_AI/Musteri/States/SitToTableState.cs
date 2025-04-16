using UnityEngine;
using BeykozEdu.FSM;

public class SitToTableState : BaseState<CustomerStateData>
{
    public override void OnEnter()
    {
        StateData.Agent.enabled = false;
        StateData.Agent.transform.position = StateData.SeatPosition.position;
        StateData.Agent.transform.LookAt(StateData.TableTarget);
        StateData.Animator.SetBool("isSitting", true);
        StateData.Agent.enabled = true;

        StateMachineHandler.AddState(new OrderState(), StateData);
    }

    public override void OnUpdate() { }

    public override void OnExit() { }
}
