using UnityEngine;
using UnityEngine.AI;
using BeykozEdu.FSM;

public class MoveToTableStateEXP : BaseState<CustomerStateDataEXP>
{
    private bool hasWarpedInUpdate = false;

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

        TryWarpToNavMesh();

        if (!StateData.Agent.isOnNavMesh)
        {
            Debug.LogError("Agent NavMesh'e yerleþtirilemedi (OnEnter sonrasý).");
            return;
        }

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
        if (StateData.Agent == null)
        {
            Debug.LogWarning("NavMeshAgent null, update durduruldu.");
            return;
        }

        if (!StateData.Agent.isOnNavMesh)
        {
            if (!hasWarpedInUpdate)
            {
                Debug.LogWarning("Agent NavMesh üzerinde deðil, Update sýrasýnda tekrar Warp deneniyor...");
                TryWarpToNavMesh();
                hasWarpedInUpdate = true;

                if (!StateData.Agent.isOnNavMesh)
                {
                    Debug.LogError("Update içinde Agent NavMesh'e alýnamadý.");
                    return;
                }

                // Gidilecek hedef tekrar atanmalý
                StateData.Agent.SetDestination(StateData.TableTarget.position);
            }
            else
            {
                Debug.LogWarning("Update içinde Warp denemesi baþarýsýz, iþlem durduruldu.");
                return;
            }
        }

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

    private void TryWarpToNavMesh()
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(StateData.Agent.transform.position, out hit, 2f, NavMesh.AllAreas))
        {
            StateData.Agent.Warp(hit.position);
            Debug.Log("Agent NavMesh'e Warp ile yerleþtirildi.");
        }
    }
}
