using UnityEngine;
using BeykozEdu.FSM;

public class MoveToTableStateEXP : BaseState<CustomerStateDataEXP>
{
    public override void OnEnter()
    {
        if (StateData.TableTarget == null || StateData.SeatPosition == null ||
            StateData.ExitTarget == null || StateData.MoneyPrefab == null ||
            StateData.MoneyPosition == null)
        {
            Debug.LogWarning("MoveToTableState baþlatýlamadý: Atamalar eksik!");
            return;
        }

        StateData.Agent.SetDestination(StateData.TableTarget.position);
        StateData.Animator.SetBool("isWalking", true);
    }

    public override void OnUpdate()
    {
        if (!StateData.Agent.pathPending && StateData.Agent.remainingDistance < 0.2f)
        {
            StateData.Animator.SetBool("isWalking", false);
            StateMachineHandler.AddState(new SitToTableStateEXP(), StateData);
        }
    }

    public override void OnExit() { }
}
