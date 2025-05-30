using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject normalNpcPrefab;
    public GameObject gamblingNpcPrefab;
    public float checkInterval = 1f;

    public Transform exitPoint;
    public GameObject moneyPrefab;

    private List<TableGroup> tables = new();
    private Dictionary<TableGroup, TableSession> tableSessions = new();

    void Start()
    {
        // Sahnedeki tüm TableModeTrigger'larý bul ve içindeki TableGroup referanslarýný al
        TableModeTrigger[] triggers = Object.FindObjectsByType<TableModeTrigger>(FindObjectsSortMode.None);

        foreach (var trigger in triggers)
        {
            if (trigger.tableGroup != null)
            {
                tables.Add(trigger.tableGroup);
            }
        }

        StartCoroutine(SpawnNPCIfAvailable());
    }

    IEnumerator SpawnNPCIfAvailable()
    {
        while (true)
        {
            foreach (var table in tables)
            {
                int emptySeats = 0;
                foreach (var seat in table.seats)
                {
                    if (!seat.IsOccupied)
                        emptySeats++;
                }

                if (emptySeats == table.seats.Length)
                {
                    TableSession session = new TableSession(table);
                    tableSessions[table] = session;

                    GameObject prefabToUse = (table.mode == TableMode.Gambling)
                        ? gamblingNpcPrefab
                        : normalNpcPrefab;

                    for (int i = 0; i < table.seats.Length; i++)
                    {
                        Seat seat = table.seats[i];
                        GameObject npc = Instantiate(prefabToUse, transform.position, Quaternion.identity);
                        CustomerFSMController controller = npc.GetComponent<CustomerFSMController>();

                        seat.Occupy();

                        controller.Init(
                            table.table,
                            seat.transform,
                            exitPoint,
                            moneyPrefab,
                            table.moneySpawnPoint
                        );

                        controller.SetAssignedSeat(seat);
                        controller.AssignSession(session);
                    }

                    break;
                }
            }

            yield return new WaitForSeconds(checkInterval);
        }
    }
}
