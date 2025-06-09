using UnityEngine;
using BeykozEdu.FSM;
using MemoryPool; // Release için MemoryPool’a eriþim

public class LeaveStateEXP : BaseState<CustomerStateDataEXP>
{
    public override void OnEnter()
    {
        StateData.Animator.SetBool("isSitting", false);  // Oturma animasyonunu baþlat
        StateData.Animator.SetBool("isWalking", true);
        StateData.Agent.SetDestination(StateData.ExitTarget.position);
    }

    public override void OnUpdate()
    {
        if (!StateData.Agent.pathPending && StateData.Agent.remainingDistance < 0.2f)
        {
            // Masa boþ olarak iþaretlenir
            MasaEXP masa = StateData.TableTarget.GetComponent<MasaEXP>();
            if (masa != null)
            {
                masa.IsEmpty = true;
            }

            // NPC'yi yok etmek yerine havuza iade et
            NPCEXP npc = StateData.Agent.GetComponent<NPCEXP>();
            if (npc != null && npc.OwnerPool != null)
            {
                //sýfýrlama burada gerçekleþecek
                StateMachineHandler.AddState(new MoveToTableStateEXP(), StateData);
                npc.OwnerPool.Release(npc);
            }
            else
            {
                Debug.LogWarning("NPC veya OwnerPool bulunamadý! GameObject yok ediliyor.");
                GameObject.Destroy(StateData.Agent.gameObject);
            }
        }
    }

    public override void OnExit() { }
}
