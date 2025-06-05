using System;
using System.Collections.Generic;

namespace MemoryPool
{
    public abstract class MemoryPoolBase<T> where T : IPoolItem<T>
    {
        protected int Size;
        protected readonly List<T> Items;

        protected MemoryPoolBase(int size)
        {
            Size = size;
            Items = new();
        }

        public virtual void Initialize()
        {
            AllocateItems(Size);
        }

        protected abstract T Create();

        public virtual T Get()
        {
            for (int i = 0; i < Size; i++)
            {
                if (Items[i].IsEnabled)
                    continue;

                Items[i].SetEnabled(true);
                return Items[i];
            }

            int allocateAmount = Math.Max(Size / 2, 1);
            AllocateItems(allocateAmount);
            Size += allocateAmount;
            T newItem = Get();
            return newItem;
        }

        public void Release(T poolItem)
        {
            poolItem.SetEnabled(false);
        }

        private void AllocateItems(int size)
        {
            for (int i = 0; i < size; i++)
            {
                Items.Add(Create());
            }
        }
    }
}