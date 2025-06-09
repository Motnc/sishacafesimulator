using UnityEngine;
using BeykozEdu.FSM;

public class WaitForFoodStateEXP : BaseState<CustomerStateDataEXP>
{
    public override void OnEnter()
    {
        Debug.Log("Müþteri yemeði bekliyor");
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