using UnityEngine;
using BeykozEdu.FSM;

public class LeaveState : BaseState<CustomerStateData>
{
    public override void OnEnter()
    {
        // Çýkýþ pozisyonuna git
        StateData.Agent.SetDestination(StateData.ExitTarget.position);
    }

    public override void OnUpdate()
    {
        // Çýkýþ pozisyonuna geldiðinde müþteri yok olmalý
        if (!StateData.Agent.pathPending && StateData.Agent.remainingDistance < 0.2f)
        {
            GameObject.Destroy(StateData.Agent.gameObject);  // Müþteri yok olur
        }
    }

    public override void OnExit() { }
}

