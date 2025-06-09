using UnityEngine;

namespace MemoryPool
{
    public class NPCPoolTestEXP : MonoBehaviour
    {
        [SerializeField] private GameObject npcPrefab;
        [SerializeField] private Transform npcPoolParent;
        [SerializeField] private Transform npcSpawnPoint;

        [Header("Otomatik Spawn Ayarý")]
        [SerializeField] private float spawnInterval = 3f;
        private float spawnTimer = 0f;

        private ObjectPoolPrefab<NPCEXP> _npcPool;

        private void Start()
        {
            // NPC havuzunu baþlat
            _npcPool = new ObjectPoolPrefab<NPCEXP>(5, npcPrefab, npcPoolParent);
            _npcPool.Initialize();

            Debug.Log("NPC Pool baþlatýldý.");
        }

        private void Update()
        {
            spawnTimer += Time.deltaTime;

            // Her frame masa kontrolü
            if (MasaManagerEXP.Instance != null && MasaManagerEXP.Instance.masalar.Count > 0)
            {
                TrySpawnNPCIfTableAvailable();
            }
        }

        private void TrySpawnNPCIfTableAvailable()
        {
            if (spawnTimer < spawnInterval)
                return;

            spawnTimer = 0f;

            MasaEXP emptyTable = MasaManagerEXP.Instance.GetEmptyMasa();
            if (emptyTable == null)
            {
                Debug.LogWarning("Boþ masa yok.");
                return;
            }

            SeatEXP emptySeat = FindEmptySeat(emptyTable);
            if (emptySeat == null)
            {
                Debug.LogWarning("Masa var ama boþ sandalye yok!");
                return;
            }

            NPCEXP npc = _npcPool.Get();
            if (npc == null)
            {
                Debug.LogWarning("NPC havuzdan alýnamadý!");
                return;
            }

            npc.SetEnabled(true);

            if (npcSpawnPoint != null)
            {
                npc.transform.position = npcSpawnPoint.position;
                npc.transform.rotation = npcSpawnPoint.rotation;
            }
            else
            {
                npc.transform.position = emptyTable.transform.position + Vector3.forward * 2f;
            }

            CustomerFSMControllerEXP customer = npc.GetComponent<CustomerFSMControllerEXP>();
            if (customer != null)
            {
                customer.Setup(
                    emptyTable.table,
                    emptySeat.transform,
                    emptyTable.exitPoint,
                    emptyTable.moneyPrefab,
                    emptyTable.moneySpawnPoint
                );
            }
            else
            {
                Debug.LogWarning("CustomerFSMController yok!");
            }

            emptyTable.IsEmpty = false;
            emptySeat.Occupy();

            Debug.Log($"NPC spawn -> Masa: {emptyTable.name}, Sandalye: {emptySeat.name}");
        }

        private SeatEXP FindEmptySeat(MasaEXP masa)
        {
            foreach (var seat in masa.seats)
            {
                if (seat != null && !seat.IsOccupied)
                    return seat;
            }
            return null;
        }
    }
}
