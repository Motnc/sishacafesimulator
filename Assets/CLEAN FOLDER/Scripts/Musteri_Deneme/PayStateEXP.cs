using UnityEngine;
using BeykozEdu.FSM;

public class PayStateEXP : BaseState<CustomerStateDataEXP>
{
    public override void OnEnter()
    {
        // Para prefab'ýný belirli pozisyona instantiate et
        GameObject.Instantiate(StateData.MoneyPrefab, StateData.MoneyPosition.position, Quaternion.identity);  // MoneyPosition kullanýlýyor
    }

    public override void OnUpdate()
    {
        // Hesap ödendikten sonra LeaveState'e geçiþ
        
        StateMachineHandler.AddState(new LeaveStateEXP(), StateData);
    }

    public override void OnExit() {
        StateData.Animator.SetBool("isSitting", false);  // Oturma animasyonunu baþlat
    }
}

