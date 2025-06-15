using UnityEngine;
using BeykozEdu.FSM;

public class OrderStateEXP : BaseState<CustomerStateDataEXP>
{
    //so þeklinde bilgi çekicez
    string[] orders = { "Nargile", "Çay", "Hamburger" };

    public override void OnEnter()
    {
        StateData.SelectedOrder = orders[Random.Range(0, orders.Length)];
        Debug.Log("Müþteri sipariþ verdi: " + StateData.SelectedOrder);

        StateData.UIController?.SetOrderText(StateData.SelectedOrder);

        StateData.OnOrderSelected?.Invoke(StateData.SelectedOrder);
        StateMachineHandler.AddState(new WaitForFoodStateEXP(), StateData);
    }

    public override void OnUpdate() { }
    public override void OnExit() { }
}