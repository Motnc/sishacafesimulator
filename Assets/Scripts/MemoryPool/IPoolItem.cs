namespace MemoryPool
{
    public interface IPoolItem<T> where T : IPoolItem<T>
    {
        public bool IsEnabled { get; }

        public MemoryPoolBase<T> OwnerPool { get; set; }

        public void OnCreate();

        public void SetEnabled(bool value);

    }
}