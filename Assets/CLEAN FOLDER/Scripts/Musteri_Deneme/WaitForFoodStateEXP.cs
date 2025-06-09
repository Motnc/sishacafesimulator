using UnityEngine;
using BeykozEdu.FSM;

public class WaitForFoodStateEXP : BaseState<CustomerStateDataEXP>
{
    public override void OnEnter()
    {
        Debug.Log("M��teri yeme�i bekliyor");
    }

    public override void OnUpdate()
    {
        if (StateData.IsOrderDelivered)
        {
            StateMachineHandler.AddState(new EatFoodStateEXP(), StateData);
        }
    }

    public override void OnExit() { }
}