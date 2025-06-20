using UnityEngine;
using BeykozEdu.FSM;

public class OrderStateEXP : BaseState<CustomerStateDataEXP>
{
    public override void OnEnter()
    {
        if (StateData.OrderDatabase == null || StateData.OrderDatabase.orders.Count == 0)
        {
            Debug.LogError("Order database bo� veya atanmam��!");
            return;
        }

        var order = StateData.OrderDatabase.orders[Random.Range(0, StateData.OrderDatabase.orders.Count)];
        StateData.SelectedOrder = order.orderName;

        Debug.Log("M��teri sipari� verdi: " + StateData.SelectedOrder);
        Debug.Log("Sprite atan�yor: " + order.orderSprite?.name);

        StateData.UIController?.SetOrderSprite(order.orderSprite);
        StateData.OnOrderSelected?.Invoke(order.orderName);

        StateMachineHandler.AddState(new WaitForFoodStateEXP(), StateData);
    }

    public override void OnUpdate() { }
    public override void OnExit() { }
}
