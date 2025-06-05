using UnityEngine;
using BeykozEdu.FSM;

public class OrderState : BaseState<CustomerStateData>
{
    string[] orders = { "Nargile", "Çay", "Hamburger" }; // çeþitlilik için geniþletildi

    public override void OnEnter()
    {
        string order = orders[Random.Range(0, orders.Length)];
        StateData.SelectedOrder = order;
        Debug.Log("Müþteri sipariþ verdi: " + order);

        StateData.FSMController.RegisterOrder(order);
        StateData.OnOrderSelected?.Invoke(order);

        StateMachineHandler.AddState(new WaitForFoodState(), StateData);
    }

    public override void OnUpdate() { }
    public override void OnExit() { }
}
