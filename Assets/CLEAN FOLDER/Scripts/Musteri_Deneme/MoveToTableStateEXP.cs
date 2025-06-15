using UnityEngine;
using UnityEngine.AI;
using BeykozEdu.FSM;

public class MoveToTableStateEXP : BaseState<CustomerStateDataEXP>
{
    public override void OnEnter()
    {
        if (StateData.TableTarget == null || StateData.SeatPosition == null ||
            StateData.ExitTarget == null || StateData.MoneyPrefab == null ||
            StateData.MoneyPosition == null)
        {
            Debug.LogWarning("MoveToTableState ba�lat�lamad�: Atamalar eksik!");
            return;
        }

        if (StateData.Agent == null)
        {
            Debug.LogError("NavMeshAgent bulunamad�.");
            return;
        }

        //  Agent NavMesh �zerinde de�ilse warp ile oturt
        if (!StateData.Agent.isOnNavMesh)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(StateData.Agent.transform.position, out hit, 2f, NavMesh.AllAreas))
            {
                StateData.Agent.Warp(hit.position); //  Do�ru y�ntem
                Debug.Log("Agent NavMesh'e Warp ile yerle�tirildi.");
            }
            else
            {
                Debug.LogError("Agent NavMesh'e yerle�tirilemedi.");
                return;
            }
        }

        //  G�zergah belirle
        bool destinationSet = StateData.Agent.SetDestination(StateData.TableTarget.position);
        if (!destinationSet)
        {
            Debug.LogError("SetDestination ba�ar�s�z oldu.");
            return;
        }

        StateData.Animator.SetBool("isWalking", true);
    }

    public override void OnUpdate()
    {
        // NavMesh�te de�ilse i�lem yap�lmaz
        if (StateData.Agent == null || !StateData.Agent.isOnNavMesh)
        {
            Debug.LogWarning("Agent NavMesh �zerinde de�il, update i�lemi durduruldu.");
            return;
        }

        // Hedefe ula�t� m�?
        if (!StateData.Agent.pathPending && StateData.Agent.remainingDistance < 0.2f)
        {
            StateData.Animator.SetBool("isWalking", false);
            StateMachineHandler.AddState(new SitToTableStateEXP(), StateData);
        }
    }

    public override void OnExit()
    {
        // Gerekirse animasyon ya da ba�ka �eyleri burada s�f�rlayabilirsin
    }
}
