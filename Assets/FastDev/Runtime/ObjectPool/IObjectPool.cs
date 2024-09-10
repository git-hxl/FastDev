
namespace FastDev
{
    public interface IObjectPool<T> where T : ObjectBase
    {
        /// <summary>
        /// 获取对象池名称。
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// 获取对象池中对象的数量。
        /// </summary>
        int Count
        {
            get;
        }

        /// <summary>
        /// 创建对象。
        /// </summary>
        /// <param name="obj">对象。</param>
        /// <param name="spawned">对象是否已被获取。</param>
        void Register(T obj, bool spawned);

        /// <summary>
        /// 检查对象。
        /// </summary>
        /// <returns>要检查的对象是否存在。</returns>
        bool CanSpawn();

        /// <summary>
        /// 获取对象。
        /// </summary>
        /// <returns>要获取的对象。</returns>
        T Spawn(string assetPath);

        /// <summary>
        /// 回收对象。
        /// </summary>
        /// <param name="obj">要回收的对象。</param>
        void Unspawn(T obj);

        /// <summary>
        /// 回收所有对象
        /// </summary>
        void UnspawnAll();

        void Release();
        void ReleaseAllUnused();
    }
}