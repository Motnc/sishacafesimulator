using UnityEngine;

namespace MemoryPool
{
    public class Enemy : IPoolItem<Enemy>
    {
        public bool IsEnabled { get; }
        public MemoryPoolBase<Enemy> OwnerPool { get; set; }

        public void OnCreate()
        {

        }

        public void Attack()
        {
            Debug.Log("Attack!!!!!!!");
        }

        public void SetEnabled(bool value)
        {
            throw new System.NotImplementedException();
        }
    }
}