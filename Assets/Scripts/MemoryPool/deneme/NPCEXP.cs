using UnityEngine;

namespace MemoryPool
{
    public class NPCEXP : MonoBehaviour, IPoolItem<NPCEXP>
    {
        public bool IsEnabled { get; private set; }
        public MemoryPoolBase<NPCEXP> OwnerPool { get; set; }

        private Vector3 _initialPosition;
        private Quaternion _initialRotation;

        private void Awake()
        {
            // NPC ilk konumunu ve rotasyonunu sakla.
            _initialPosition = transform.position;
            _initialRotation = transform.rotation;
        }

        public void OnCreate()
        {
            // Havuzda olu�turulurken pasif ba�las�n.
            gameObject.SetActive(false);
        }

        public void SetEnabled(bool value)
        {
            IsEnabled = value;
            gameObject.SetActive(value);

            if (!value)
            {
                // Pasifle�tirildi�inde NPC'yi ilk konumuna getir.
                transform.position = _initialPosition;
                transform.rotation = _initialRotation;
            }
        }

        public void Speak()
        {
            Debug.Log("NPC: Merhaba!");
        }

        private void OnBecameInvisible()
        {
            // Ekrandan ��kt���nda havuza geri g�nder.
            OwnerPool.Release(this);
        }
    }
}
