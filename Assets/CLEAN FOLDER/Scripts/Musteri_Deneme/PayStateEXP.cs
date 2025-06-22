using UnityEngine;
using BeykozEdu.FSM;

public class PayStateEXP : BaseState<CustomerStateDataEXP>
{
    public override void OnEnter()
    {
        Debug.Log("PayStateEXP OnEnter baþladý.");

        string orderedFood = StateData.SelectedOrder;
        int price = GetPriceForFood(orderedFood);

        GameObject moneyObj = GameObject.Instantiate(StateData.MoneyPrefab, StateData.MoneyPosition.position, Quaternion.identity);
        MoneyGain moneyGain = moneyObj.GetComponent<MoneyGain>();
        if (moneyGain != null) moneyGain.SetAmount(price);
        Debug.Log($"Para objesi oluþturuldu. Sipariþ: {orderedFood}, Kazanç: {price}");

        if (StateData.DirtyPrefab == null)
        {
            Debug.LogWarning("DirtyPrefab NULL!");
        }
        if (StateData.DirtyPosition == null)
        {
            Debug.LogWarning("DirtyPosition NULL!");
        }

        // Burayý kesinlikle çaðýr.
        if (StateData.DirtyPrefab != null && StateData.DirtyPosition != null)
        {
            var dirtyObj = GameObject.Instantiate(StateData.DirtyPrefab, StateData.DirtyPosition.position, StateData.DirtyPosition.rotation);
            Debug.Log("Kirli obje oluþturuldu: " + dirtyObj.name);
        }
        else
        {
            Debug.LogWarning("Kirli obje oluþturulamadý, prefab ya da pozisyon null!");
        }
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
