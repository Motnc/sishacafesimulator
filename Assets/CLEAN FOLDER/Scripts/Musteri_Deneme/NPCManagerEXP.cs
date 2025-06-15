using UnityEngine;

public class NPCManagerEXP : MonoBehaviour
{
    [Header("Setup Defaults")]
    [SerializeField] private Transform defaultTableTarget;
    [SerializeField] private Transform defaultSeatPosition;
    [SerializeField] private Transform defaultExitPoint;
    [SerializeField] private GameObject defaultMoneyPrefab;
    [SerializeField] private Transform defaultMoneyPosition;

    private void Update()
    {
        GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC");

        foreach (GameObject npcObj in npcs)
        {
            CustomerFSMControllerEXP customer = npcObj.GetComponent<CustomerFSMControllerEXP>();
            CustomerUIController ui = npcObj.GetComponentInChildren<CustomerUIController>();

            if (customer != null)
            {
                if (customer.TableTarget == null)
                {
                    customer.Setup(
                        defaultTableTarget,
                        defaultSeatPosition,
                        defaultExitPoint,
                        defaultMoneyPrefab,
                        defaultMoneyPosition,
                        ui
                    );
                }
            }
        }
    }
}
