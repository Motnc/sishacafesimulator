using UnityEngine;

namespace MemoryPool
{
    public class Bullet : MonoBehaviour, IPoolItem<Bullet>
    {
        public bool IsEnabled => _enabled;
        public MemoryPoolBase<Bullet> OwnerPool { get; set; }

        private bool _enabled;
        private Rigidbody _rigidbody;
        private Vector3 _initialPosition;
        private Quaternion _initialRotation;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _initialPosition = transform.position;
            _initialRotation = transform.rotation;
        }

        public void OnCreate()
        {
            gameObject.SetActive(false);
        }

        public void SetEnabled(bool value)
        {
            _enabled = value;
            gameObject.SetActive(value);

            if (!_enabled)
            {
                transform.position = _initialPosition;
                transform.rotation = _initialRotation;
                _rigidbody.linearVelocity = Vector3.zero;
                _rigidbody.angularVelocity = Vector3.zero;
            }
        }

        public void Use()
        {
            _rigidbody.linearVelocity = transform.forward * 10;
        }

        private void OnBecameInvisible()
        {
            OwnerPool.Release(this);
        }
    }
}