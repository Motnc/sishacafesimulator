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
            // NPC havuzunu ba�lat.
            _npcPool = new ObjectPoolPrefab<NPC>(5, npcPrefab, npcPoolParent);
            _npcPool.Initialize();
        }

        private void Update()
        {
            // Bo�luk tu�una bas�nca havuzdan yeni bir NPC ��kar ve kullan.
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var npc = _npcPool.Get();
                npc.Speak(); // �rnek fonksiyon.
            }

            
        }
    }
}
