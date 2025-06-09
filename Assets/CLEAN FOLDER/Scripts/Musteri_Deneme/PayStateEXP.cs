using UnityEngine;
using BeykozEdu.FSM;

public class PayStateEXP : BaseState<CustomerStateDataEXP>
{
    public override void OnEnter()
    {
        // Para prefab'�n� belirli pozisyona instantiate et
        GameObject.Instantiate(StateData.MoneyPrefab, StateData.MoneyPosition.position, Quaternion.identity);  // MoneyPosition kullan�l�yor
    }

    public override void OnUpdate()
    {
        // Hesap �dendikten sonra LeaveState'e ge�i�
        
        StateMachineHandler.AddState(new LeaveStateEXP(), StateData);
    }

    public override void OnExit() {
        StateData.Animator.SetBool("isSitting", false);  // Oturma animasyonunu ba�lat
    }
}

