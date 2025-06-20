using UnityEngine;
using BeykozEdu.FSM;

public class OrderStateEXP : BaseState<CustomerStateDataEXP>
{
    public override void OnEnter()
    {
        if (StateData.OrderDatabase == null || StateData.OrderDatabase.orders.Count == 0)
        {
            Debug.LogError("Order database boþ veya atanmamýþ!");
            return;
        }

        var order = StateData.OrderDatabase.orders[Random.Range(0, StateData.OrderDatabase.orders.Count)];
        StateData.SelectedOrder = order.orderName;

        Debug.Log("Müþteri sipariþ verdi: " + StateData.SelectedOrder);
        Debug.Log("Sprite atanýyor: " + order.orderSprite?.name);

        StateData.UIController?.SetOrderSprite(order.orderSprite);
        StateData.OnOrderSelected?.Invoke(order.orderName);

        StateMachineHandler.AddState(new WaitForFoodStateEXP(), StateData);
    }

    public override void OnUpdate() { }
    public override void OnExit() { }
}
