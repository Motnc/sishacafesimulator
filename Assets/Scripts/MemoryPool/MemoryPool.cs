namespace MemoryPool
{
    public class MemoryPool<T> : MemoryPoolBase<T> where T : IPoolItem<T>, new()
    {
        public MemoryPool(int size) : base(size)
        {
        }

        protected override T Create()
        {
            var obj = new T
            {
                OwnerPool = this
            };
            obj.OnCreate();
            return obj;
        }
    }
}