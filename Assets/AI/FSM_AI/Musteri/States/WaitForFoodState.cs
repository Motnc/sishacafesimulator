using UnityEngine;
using BeykozEdu.FSM;

public class WaitForFoodState : BaseState<CustomerStateData>
{
    public override void OnEnter()
    {
        Debug.Log("M��teri yeme�i bekliyor");
    }

    public override void OnUpdate()
    {
        if (StateData.IsOrderDelivered)
        {
            StateMachineHandler.AddState(new EatFoodState(), StateData);
        }
    }

    public override void OnExit() { }
}