using UnityEngine;
using BeykozEdu.FSM;

public class PayState : BaseState<CustomerStateData>
{
    public override void OnEnter()
    {
        GameObject.Instantiate(StateData.MoneyPrefab, StateData.MoneyPosition.position, Quaternion.identity);
    }

    public override void OnUpdate()
    {
        StateMachineHandler.AddState(new LeaveState(), StateData);
    }

    public override void OnExit()
    {
        StateData.Animator.SetBool("isSitting", false); 
    }
}

