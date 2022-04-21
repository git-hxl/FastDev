using System.Collections.Generic;
namespace FastDev
{
    /// <summary>
    /// 用于非GameObject对象的对象池
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Pool<T> : Singleton<Pool<T>>  where T : IPoolable, new()
    {
        private uint mMaxCount = 99;//缓存池最大个数
        protected readonly Stack<T> mCacheStack = new Stack<T>();//缓存池
        //当前缓存池中对象个数
        public int Count
        {
            get { return mCacheStack.Count; }
        }
        /// <summary>
        /// 分配
        /// </summary>
        /// <returns></returns>
        public T Allocate()
        {
            var result = mCacheStack.Count == 0 ? new T() : mCacheStack.Pop();
            result.IsRecycled = false;
            result.OnAllocated();
            return result;
        }
        /// <summary>
        /// 回收
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Recycle(T obj)
        {
            if (obj == null || obj.IsRecycled)
            {
                return false;
            }
            if (mCacheStack.Count >= mMaxCount)
            {
                obj = default;
                return false;
            }
            obj.IsRecycled = true;
            obj.OnRecycled();
            mCacheStack.Push(obj);
            return true;
        }
    }
}