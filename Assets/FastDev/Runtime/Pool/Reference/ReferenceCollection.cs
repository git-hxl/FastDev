using System;
using System.Collections.Generic;

namespace FastDev
{
    public class ReferenceCollection
    {
        private readonly Queue<IReference> references = new Queue<IReference>();

        private Type referenceType;

        public int CurUsingRefCount { get; private set; }
        public int AcquireRefCount { get; private set; }
        public int ReleaseRefCount { get; private set; }
        public int AddRefCount { get; private set; }
        public int RemoveRefCount { get; private set; }

        public ReferenceCollection(Type type)
        {
            referenceType = type;

            CurUsingRefCount = 0;
            AcquireRefCount = 0;
            ReleaseRefCount = 0;
            AddRefCount = 0;
            RemoveRefCount = 0;
        }

        public T Acquire<T>() where T : IReference, new()
        {
            if (typeof(T) != referenceType)
            {
                throw new Exception("请求类型错误");
            }
            CurUsingRefCount++;
            AcquireRefCount++;

            lock (references)
            {
                if (references.Count > 0)
                {
                    return (T)references.Dequeue();
                }
            }

            AddRefCount++;
            return new T();
        }

        public void Release(IReference reference)
        {
            reference.OnClear();
            lock (references)
            {
                if (references.Contains(reference))
                {
                    throw new Exception("重复释放");
                }

                references.Enqueue(reference);
            }
            CurUsingRefCount--;
            ReleaseRefCount++;
        }

        public void Add<T>(int count) where T : IReference, new()
        {
            if (typeof(T) != referenceType)
            {
                throw new Exception("请求类型错误");
            }
            lock (references)
            {
                AddRefCount += count;
                while (count-- > 0)
                {
                    references.Enqueue(new T());
                }
            }
        }

        public void Remove(int count)
        {
            lock (references)
            {
                if (count > references.Count)
                {
                    count = references.Count;
                }

                RemoveRefCount += count;

                while(count-- > 0)
                {
                    references.Dequeue();
                }
            }
        }

        public void RemoveAll()
        {
            lock(references)
            {
                RemoveRefCount += references.Count;
                references.Clear();
            }
        }
    }
}
