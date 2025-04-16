using UnityEngine;
using BeykozEdu.FSM;

public class OrderState : BaseState<CustomerStateData>
{
    string[] orders = { "Nargile" };

    public override void OnEnter()
    {
        StateData.SelectedOrder = orders[Random.Range(0, orders.Length)];
        Debug.Log("Müþteri sipariþ verdi: " + StateData.SelectedOrder);
        StateData.OnOrderSelected?.Invoke(StateData.SelectedOrder);
        StateMachineHandler.AddState(new WaitForFoodState(), StateData);
    }

    public override void OnUpdate() { }
    public override void OnExit() { }
}