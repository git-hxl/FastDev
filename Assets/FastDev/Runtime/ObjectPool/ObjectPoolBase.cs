
namespace FastDev
{
    public abstract class ObjectPoolBase
    {
        public abstract void Release();
        public abstract void ReleaseAllUnused();

        internal abstract void Update();

        internal abstract void Shutdown();
    }
}
