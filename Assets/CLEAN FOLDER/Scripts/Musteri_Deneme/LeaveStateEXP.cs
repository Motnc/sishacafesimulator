using UnityEngine;
using BeykozEdu.FSM;
using MemoryPool; // Release için MemoryPool’a eriþim

public class LeaveStateEXP : BaseState<CustomerStateDataEXP>
{
    public override void OnEnter()
    {
        StateData.Animator.SetBool("isSitting", false);  // Oturma animasyonunu kapat
        StateData.Animator.SetBool("isWalking", true);
        StateData.Agent.SetDestination(StateData.ExitTarget.position);
    }

    public override void OnUpdate()
    {
        if (!StateData.Agent.pathPending && StateData.Agent.remainingDistance < 0.2f)
        {
            // Masa boþ olarak iþaretlenir (Güvenli þekilde)
            if (StateData.TableTarget != null)
            {
                Transform masaTransform = StateData.TableTarget;

                MasaEXP masa = masaTransform.GetComponent<MasaEXP>() ??
                               masaTransform.GetComponentInParent<MasaEXP>() ??
                               masaTransform.GetComponentInChildren<MasaEXP>();

                if (masa != null)
                {
                    masa.IsEmpty = true;
                    Debug.Log($"{masa.name} boþ olarak iþaretlendi.");
                }
                else
                {
                    Debug.LogWarning($"MasaEXP bulunamadý! TableTarget objesi: {masaTransform.name}");
                }
            }
            else
            {
                Debug.LogWarning("TableTarget null, masa boþ olarak iþaretlenemedi.");
            }

            // NPC'yi yok etmek yerine havuza iade et
            NPCEXP npc = StateData.Agent.GetComponent<NPCEXP>();
            if (npc != null && npc.OwnerPool != null)
            {
                // Ýsteðe baðlý: State sýfýrlama vb. burada yapýlabilir
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
