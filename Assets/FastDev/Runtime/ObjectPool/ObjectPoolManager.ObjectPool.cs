using System;
using System.Collections.Generic;
using static UnityEditor.Progress;

namespace FastDev
{
    public sealed partial class ObjectPoolManager
    {
        public class ObjectPool<T> : ObjectPoolBase, IObjectPool<T> where T : ObjectBase
        {
            private readonly List<T> m_Objects;
            private readonly List<T> m_CachedCanReleaseObjects;
            private readonly List<T> m_CachedToReleaseObjects;

            private string m_Name;
            private float m_AutoReleaseInterval;
            private int m_Capacity;
            private float m_ExpireTime;
            private float m_AutoReleaseTime;

            /// <summary>
            /// 初始化对象池
            /// </summary>
            /// <param name="name">对象池名称</param>
            /// <param name="autoReleaseInterval">对象池自动释放可释放对象的间隔秒数</param>
            /// <param name="capacity">对象池的容量</param>
            /// <param name="expireTime">对象池对象过期秒数</param>
            public ObjectPool(string name, float autoReleaseInterval, int capacity, float expireTime)
            {
                m_Objects = new List<T>();

                m_CachedCanReleaseObjects = new List<T>();
                m_CachedToReleaseObjects = new List<T>();

                m_Name = name;
                m_AutoReleaseInterval = autoReleaseInterval;
                m_ExpireTime = expireTime;
                m_Capacity = capacity;
                m_AutoReleaseTime = 0;
            }

            /// <summary>
            /// 获取对象池名称。
            /// </summary>
            public string Name
            {
                get
                {
                    return m_Name;
                }
            }

            /// <summary>
            /// 获取对象池的容量。
            /// </summary>
            public int Capacity
            {
                get
                {
                    return m_Capacity;
                }
            }

            // <summary>
            /// 获取对象池中对象的数量。
            /// </summary>
            public int Count
            {
                get
                {
                    return m_Objects.Count;
                }
            }

            /// <summary>
            /// 创建对象
            /// </summary>
            /// <param name="obj"></param>
            /// <param name="spawned"></param>
            public void Register(T obj, bool spawned)
            {
                if (obj == null)
                {
                    throw new Exception("Object is invalid.");
                }

                if (obj.Target == null)
                {
                    throw new Exception("Target is invalid.");
                }

                m_Objects.Add(obj);

                if (spawned)
                {
                    obj.IsInUse = true;
                    obj.LastUseTime = DateTime.UtcNow;
                    obj.OnSpawn();
                }

                if (m_Objects.Count > m_Capacity)
                {
                    Release();
                }
            }

            /// <summary>
            /// 判断是否可以获取对象
            /// </summary>
            /// <returns></returns>
            public bool CanSpawn()
            {
                foreach (T item in m_Objects)
                {
                    if (item.IsInUse == false)
                    {
                        return true;
                    }
                }
                return false;
            }


            /// <summary>
            /// 获取对象。
            /// </summary>
            /// <returns>要获取的对象。</returns>
            public T Spawn()
            {
                foreach (T item in m_Objects)
                {
                    if (item.IsInUse == false)
                    {
                        item.IsInUse = true;
                        item.LastUseTime = DateTime.UtcNow;
                        item.OnSpawn();
                        return item;
                    }
                }

                return null;
            }

            /// <summary>
            /// 回收对象。
            /// </summary>
            /// <param name="obj">要回收的对象。</param>
            public void Unspawn(T obj)
            {
                if (obj == null)
                {
                    throw new Exception("Object is invalid.");
                }

                if (obj.Target == null)
                {
                    throw new Exception("Target is invalid.");
                }

                obj.OnUnspawn();
                obj.LastUseTime = DateTime.UtcNow;
                obj.IsInUse = false;
            }

            /// <summary>
            /// 回收所有对象
            /// </summary>
            public void UnspawnAll()
            {
                for (int i = 0; i < m_Objects.Count; i++)
                {
                    if (m_Objects[i].IsInUse)
                    {
                        var obj = m_Objects[i];
                        Unspawn(obj);
                    }
                }
            }

            /// <summary>
            /// 释放对象池中的可释放对象。
            /// </summary>
            public override void Release()
            {
                m_AutoReleaseTime = 0f;

                int releaseCount = m_Objects.Count - m_Capacity;

                if (releaseCount <= 0)
                {
                    return;
                }

                GetCanReleaseObjects(m_CachedCanReleaseObjects);

                releaseCount = Math.Min(releaseCount, m_CachedCanReleaseObjects.Count);

                if (releaseCount <= 0)
                {
                    return;
                }

                DateTime expireTime = DateTime.UtcNow.AddSeconds(-m_ExpireTime);

                m_CachedToReleaseObjects.Clear();

                for (int i = 0; i < releaseCount; i++)
                {
                    if (m_CachedCanReleaseObjects[i].LastUseTime <= expireTime)
                    {
                        m_CachedToReleaseObjects.Add(m_CachedCanReleaseObjects[i]);
                    }
                }

                if (m_CachedToReleaseObjects == null || m_CachedToReleaseObjects.Count <= 0)
                {
                    return;
                }

                foreach (T toReleaseObject in m_CachedToReleaseObjects)
                {
                    ReleaseObject(toReleaseObject);
                }
            }

            /// <summary>
            /// 释放对象池中的所有未使用对象。
            /// </summary>
            public override void ReleaseAllUnused()
            {
                GetCanReleaseObjects(m_CachedCanReleaseObjects);
                foreach (T toReleaseObject in m_CachedCanReleaseObjects)
                {
                    ReleaseObject(toReleaseObject);
                }
                m_AutoReleaseTime = 0f;
            }

            /// <summary>
            /// 释放对象。
            /// </summary>
            /// <param name="obj">要释放的对象。</param>
            /// <returns>释放对象是否成功。</returns>
            public bool ReleaseObject(T obj)
            {
                if (obj == null)
                {
                    throw new Exception("Object is invalid.");
                }

                if (obj.Target == null)
                {
                    throw new Exception("Target is invalid.");
                }


                if (obj.IsInUse)
                {
                    return false;
                }

                m_Objects.Remove(obj);

                ReferencePool.Release(obj);

                return true;
            }

            private void GetCanReleaseObjects(List<T> results)
            {
                if (results == null)
                {
                    throw new Exception("Results is invalid.");
                }

                results.Clear();

                foreach (T item in m_Objects)
                {
                    if (item.IsInUse)
                    {
                        continue;
                    }

                    results.Add(item);
                }
            }

            internal override void Update(float elapseSeconds, float realElapseSeconds)
            {
                m_AutoReleaseTime += realElapseSeconds;

                if (m_AutoReleaseTime < m_AutoReleaseInterval)
                {
                    return;
                }
                Release();
            }


            internal override void Shutdown()
            {
                foreach (T item in m_Objects)
                {
                    ReferencePool.Release(item);
                }

                m_Objects.Clear();

                m_CachedCanReleaseObjects.Clear();
                m_CachedToReleaseObjects.Clear();
            }

        }
    }
}
