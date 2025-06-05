using System;
using UnityEngine;

namespace MemoryPool
{
    public class MemoryPoolTest : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform bulletPoolParent;

        private ObjectPoolPrefab<Bullet> _bulletPool;

        private void Start()
        {
            _bulletPool = new ObjectPoolPrefab<Bullet>(1, bulletPrefab, bulletPoolParent);
            _bulletPool.Initialize();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var bullet = _bulletPool.Get();
                bullet.Use();
            }
        }
    }
}