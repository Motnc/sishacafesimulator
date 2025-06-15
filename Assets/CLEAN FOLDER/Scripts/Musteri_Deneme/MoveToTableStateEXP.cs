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
            Debug.LogWarning("MoveToTableState baþlatýlamadý: Atamalar eksik!");
            return;
        }

        if (StateData.Agent == null)
        {
            Debug.LogError("NavMeshAgent bulunamadý.");
            return;
        }

        //  Agent NavMesh üzerinde deðilse warp ile oturt
        if (!StateData.Agent.isOnNavMesh)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(StateData.Agent.transform.position, out hit, 2f, NavMesh.AllAreas))
            {
                StateData.Agent.Warp(hit.position); //  Doðru yöntem
                Debug.Log("Agent NavMesh'e Warp ile yerleþtirildi.");
            }
            else
            {
                Debug.LogError("Agent NavMesh'e yerleþtirilemedi.");
                return;
            }
        }

        //  Güzergah belirle
        bool destinationSet = StateData.Agent.SetDestination(StateData.TableTarget.position);
        if (!destinationSet)
        {
            Debug.LogError("SetDestination baþarýsýz oldu.");
            return;
        }

        StateData.Animator.SetBool("isWalking", true);
    }

    public override void OnUpdate()
    {
        // NavMesh’te deðilse iþlem yapýlmaz
        if (StateData.Agent == null || !StateData.Agent.isOnNavMesh)
        {
            Debug.LogWarning("Agent NavMesh üzerinde deðil, update iþlemi durduruldu.");
            return;
        }

        // Hedefe ulaþtý mý?
        if (!StateData.Agent.pathPending && StateData.Agent.remainingDistance < 0.2f)
        {
            StateData.Animator.SetBool("isWalking", false);
            StateMachineHandler.AddState(new SitToTableStateEXP(), StateData);
        }
    }

    public override void OnExit()
    {
        // Gerekirse animasyon ya da baþka þeyleri burada sýfýrlayabilirsin
    }
}
