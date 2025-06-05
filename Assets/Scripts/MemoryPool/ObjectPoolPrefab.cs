using UnityEngine;

namespace MemoryPool
{
    public class ObjectPoolPrefab<T> : MemoryPoolBase<T> where T : MonoBehaviour, IPoolItem<T>
    {
        private readonly GameObject _prefab;
        private readonly Transform _parent;

        public ObjectPoolPrefab(int size, GameObject prefab, Transform parent) : base(size)
        {
            _prefab = prefab;
            _parent = parent;
        }

        protected override T Create()
        {
            T obj = Object.Instantiate(_prefab).GetComponent<T>();
            obj.transform.SetParent(_parent);
            obj.OwnerPool = this;
            obj.OnCreate();
            return obj;
        }
    }
}