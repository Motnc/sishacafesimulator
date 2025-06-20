using UnityEngine;
using BeykozEdu.FSM;

public class WaitForFoodStateEXP : BaseState<CustomerStateDataEXP>
{
    private float timer = 0f;
    private Sprite stage1, stage2, stage3;
    private bool stage2Set = false;
    private bool stage3Set = false;

    public override void OnEnter()
    {
        Debug.Log("Müþteri yemeði bekliyor");
        timer = 0f;
        StateData.UIController?.ClearEmotion();

        var selectedOrder = StateData.SelectedOrder;
        var orderData = StateData.OrderDatabase.orders.Find(o => o.orderName == selectedOrder);

        if (orderData != null)
        {
            stage1 = orderData.waitStage1;
            stage2 = orderData.waitStage2;
            stage3 = orderData.waitStage3;

            if (stage1 != null)
                StateData.UIController?.SetEmotionSprite(stage1);
        }
        else
        {
            Debug.LogWarning("Bekleme sprite'larý atanamadý. OrderData bulunamadý: " + selectedOrder);
        }
    }

    public override void OnUpdate()
    {
        Debug.Log("WaitForFoodStateEXP Update çalýþýyor, timer: " + timer);
        timer += Time.deltaTime;

        if (timer > 30f && !stage3Set)
        {
            StateData.UIController?.SetEmotionSprite(stage3);
            stage3Set = true;
        }
        else if (timer > 15f && !stage2Set)
        {
            StateData.UIController?.SetEmotionSprite(stage2);
            stage2Set = true;
        }

        if (StateData.IsOrderDelivered)
        {
            StateMachineHandler.AddState(new EatFoodStateEXP(), StateData);
        }
    }

    public override void OnExit()
    {
        Debug.Log("Yemek geldi, tepkiler sýfýrlandý");
        StateData.UIController?.ClearEmotion();
    }
}
