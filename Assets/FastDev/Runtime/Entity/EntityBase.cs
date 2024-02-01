
namespace FastDev
{
    public abstract class EntityBase : IReference
    {
        public abstract int EntityID { get; }

        public abstract void Clear();
    }
}
