using UnityEngine;
using BeykozEdu.FSM;

public class LeaveState : BaseState<CustomerStateData>
{
    public override void OnEnter()
    {
        // ��k�� pozisyonuna git
        StateData.Agent.SetDestination(StateData.ExitTarget.position);
    }

    public override void OnUpdate()
    {
        // ��k�� pozisyonuna geldi�inde m��teri yok olmal�
        if (!StateData.Agent.pathPending && StateData.Agent.remainingDistance < 0.2f)
        {
            GameObject.Destroy(StateData.Agent.gameObject);  // M��teri yok olur
        }
    }

    public override void OnExit() { }
}

