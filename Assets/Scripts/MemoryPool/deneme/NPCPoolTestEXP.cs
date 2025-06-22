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

        [Header("Sabit Çýkýþ Noktasý")]
        [SerializeField] private Transform globalExitPoint; //  yeni alan

        private ObjectPoolPrefab<NPCEXP> _npcPool;
        private bool isSpawningActive = false;

        private void Start()
        {
            _npcPool = new ObjectPoolPrefab<NPCEXP>(5, npcPrefab, npcPoolParent);
            _npcPool.Initialize();
            Debug.Log("NPC Pool baþlatýldý.");
        }

        private void Update()
        {
            if (!isSpawningActive) return;

            spawnTimer += Time.deltaTime;

            if (spawnTimer >= spawnInterval)
            {
                spawnTimer = 0f;
                TrySpawnNPCIfTableAvailable();
            }
        }

        public void StartSpawning()
        {
            isSpawningActive = true;
        }

        public void StopSpawning()
        {
            isSpawningActive = false;
        }

        private void TrySpawnNPCIfTableAvailable()
        {
            if (MasaManagerEXP.Instance == null || MasaManagerEXP.Instance.masalar.Count == 0)
            {
                Debug.LogWarning("MasaManager yok ya da masa sayýsý 0.");
                return;
            }

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
            CustomerUIController ui = npc.GetComponentInChildren<CustomerUIController>();

            if (customer != null)
            {
                if (globalExitPoint == null)
                {
                    Debug.LogError("globalExitPoint sahnede atanmamýþ!");
                    return;
                }

                customer.Setup(
                    emptyTable.table,
                    emptySeat.transform,
                    globalExitPoint, //  sabit çýkýþ noktasý
                    emptyTable.moneyPrefab,
                    emptyTable.dirtyPrefab,
                    emptyTable.moneySpawnPoint,
                    emptyTable.dirtySpawnPoint,
                    ui
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
