using UnityEngine;

namespace MemoryPool
{
    public class NPCPoolTest : MonoBehaviour
    {
        [SerializeField] private GameObject npcPrefab;
        [SerializeField] private Transform npcPoolParent;

        private ObjectPoolPrefab<NPC> _npcPool;

        private void Start()
        {
            // NPC havuzunu baþlat.
            _npcPool = new ObjectPoolPrefab<NPC>(5, npcPrefab, npcPoolParent);
            _npcPool.Initialize();
        }

        private void Update()
        {
            // Boþluk tuþuna basýnca havuzdan yeni bir NPC çýkar ve kullan.
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var npc = _npcPool.Get();
                npc.Speak(); // Örnek fonksiyon.
            }

            
        }
    }
}
