
namespace FastDev
{
    public abstract class ObjectPoolBase
    {
        public abstract void Release();

        internal abstract void Update(float elapseSeconds, float realElapseSeconds);

        internal abstract void Shutdown();
    }
}
