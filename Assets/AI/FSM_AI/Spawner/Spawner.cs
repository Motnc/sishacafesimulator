using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject npcPrefab;
    public float checkInterval = 1f;

    public List<TableGroup> tables = new List<TableGroup>();
    public Transform exitPoint;
    public GameObject moneyPrefab;

    private Dictionary<TableGroup, TableSession> tableSessions = new();

    void Start()
    {
<<<<<<< HEAD
        // Sahnedeki t�m TableModeTrigger'lar� bul ve i�indeki TableGroup referanslar�n� al
        TableModeTrigger[] triggers = Object.FindObjectsByType<TableModeTrigger>(FindObjectsSortMode.None);

        foreach (var trigger in triggers)
        {
            if (trigger.tableGroup != null)
            {
                tables.Add(trigger.tableGroup);
            }
        }

=======
>>>>>>> origin/ozexe.outro
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

                // Masa tamamen bo�sa (�rne�in 4 koltuk varsa ve hepsi bo�sa)
                if (emptySeats == table.seats.Length)
                {
                    // Yeni masa oturumu olu�tur
                    TableSession session = new TableSession(table);
                    tableSessions[table] = session;

                    for (int i = 0; i < table.seats.Length; i++)
                    {
                        Seat seat = table.seats[i];
                        GameObject npc = Instantiate(npcPrefab, transform.position, Quaternion.identity);
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

                    break; // Bu masa doldu, s�radaki frame'e ge�
                }
            }

            yield return new WaitForSeconds(checkInterval);
        }
    }
}
