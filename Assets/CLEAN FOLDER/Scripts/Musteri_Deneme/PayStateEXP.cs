using UnityEngine;
using BeykozEdu.FSM;

public class PayStateEXP : BaseState<CustomerStateDataEXP>
{
    public override void OnEnter()
    {
        string orderedFood = StateData.SelectedOrder;
        int price = GetPriceForFood(orderedFood);

        GameObject moneyObj = GameObject.Instantiate(StateData.MoneyPrefab, StateData.MoneyPosition.position, Quaternion.identity);

        // MoneyGain varsa fiyat� ata
        MoneyGain moneyGain = moneyObj.GetComponent<MoneyGain>();
        if (moneyGain != null)
        {
            moneyGain.SetAmount(price);
        }

        Debug.Log($"Para objesi olu�turuldu. Sipari�: {orderedFood}, Kazan�: {price}");
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
            case "�ay": return 60;
            default: return 40;
        }
    }
}
