
namespace FastDev
{
    public abstract class ObjectPoolBase
    {
        public abstract void Release();
        public abstract void ReleaseAllUnused();

        internal abstract void Update(float elapseSeconds, float realElapseSeconds);

        internal abstract void Shutdown();
    }
}
