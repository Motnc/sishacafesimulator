using UnityEngine;
using BeykozEdu.FSM;

public class OrderStateEXP : BaseState<CustomerStateDataEXP>
{
    //so �eklinde bilgi �ekicez
    string[] orders = { "Nargile", "�ay", "Hamburger" };

    public override void OnEnter()
    {
        StateData.SelectedOrder = orders[Random.Range(0, orders.Length)];
        Debug.Log("M��teri sipari� verdi: " + StateData.SelectedOrder);

        StateData.UIController?.SetOrderText(StateData.SelectedOrder);

        StateData.OnOrderSelected?.Invoke(StateData.SelectedOrder);
        StateMachineHandler.AddState(new WaitForFoodStateEXP(), StateData);
    }

    public override void OnUpdate() { }
    public override void OnExit() { }
}