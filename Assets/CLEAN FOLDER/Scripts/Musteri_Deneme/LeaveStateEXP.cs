using UnityEngine;
using BeykozEdu.FSM;
using MemoryPool; // Release i�in MemoryPool�a eri�im

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
            // Masa bo� olarak i�aretlenir (G�venli �ekilde)
            if (StateData.TableTarget != null)
            {
                Transform masaTransform = StateData.TableTarget;

                MasaEXP masa = masaTransform.GetComponent<MasaEXP>() ??
                               masaTransform.GetComponentInParent<MasaEXP>() ??
                               masaTransform.GetComponentInChildren<MasaEXP>();

                if (masa != null)
                {
                    masa.IsEmpty = true;
                    Debug.Log($"{masa.name} bo� olarak i�aretlendi.");
                }
                else
                {
                    Debug.LogWarning($"MasaEXP bulunamad�! TableTarget objesi: {masaTransform.name}");
                }
            }
            else
            {
                Debug.LogWarning("TableTarget null, masa bo� olarak i�aretlenemedi.");
            }

            // NPC'yi yok etmek yerine havuza iade et
            NPCEXP npc = StateData.Agent.GetComponent<NPCEXP>();
            if (npc != null && npc.OwnerPool != null)
            {
                // �ste�e ba�l�: State s�f�rlama vb. burada yap�labilir
                StateMachineHandler.AddState(new MoveToTableStateEXP(), StateData);
                npc.OwnerPool.Release(npc);
            }
            else
            {
                Debug.LogWarning("NPC veya OwnerPool bulunamad�! GameObject yok ediliyor.");
                GameObject.Destroy(StateData.Agent.gameObject);
            }
        }
    }

    public override void OnExit() { }
}
