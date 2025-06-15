using UnityEngine;
using BeykozEdu.FSM;

public class PayStateEXP : BaseState<CustomerStateDataEXP>
{
    public override void OnEnter()
    {
        string orderedFood = StateData.SelectedOrder;
        int price = GetPriceForFood(orderedFood);

        GameObject moneyObj = GameObject.Instantiate(StateData.MoneyPrefab, StateData.MoneyPosition.position, Quaternion.identity);

        // MoneyGain varsa fiyatý ata
        MoneyGain moneyGain = moneyObj.GetComponent<MoneyGain>();
        if (moneyGain != null)
        {
            moneyGain.SetAmount(price);
        }

        Debug.Log($"Para objesi oluþturuldu. Sipariþ: {orderedFood}, Kazanç: {price}");
    }

    public override void OnUpdate()
    {
        StateMachineHandler.AddState(new LeaveStateEXP(), StateData);
    }

    public override void OnExit()
    {
        StateData.Animator.SetBool("isSitting", false);
    }

    private int GetPriceForFood(string food)
    {
        switch (food)
        {
            case "Nargile": return 75;
            case "Hamburger": return 50;
            case "Çay": return 60;
            default: return 40;
        }
    }
}
