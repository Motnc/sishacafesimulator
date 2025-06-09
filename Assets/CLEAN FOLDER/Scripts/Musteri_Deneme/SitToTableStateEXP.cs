using UnityEngine;
using BeykozEdu.FSM;

public class SitToTableStateEXP : BaseState<CustomerStateDataEXP>
{
    public override void OnEnter()
    {
        StateData.Agent.enabled = false;
        StateData.Agent.transform.position = StateData.SeatPosition.position;
        StateData.Agent.transform.LookAt(StateData.TableTarget);
        StateData.Animator.SetBool("isSitting", true);
        StateData.Agent.enabled = true;

        // Masa'yý dolu iþaretle
        MasaEXP masa = StateData.TableTarget.GetComponent<MasaEXP>();
        if (masa != null)
        {
            masa.IsEmpty = false;
        }

        StateMachineHandler.AddState(new OrderStateEXP(), StateData);
    }

    public override void OnUpdate() { }

    public override void OnExit() { }
}
