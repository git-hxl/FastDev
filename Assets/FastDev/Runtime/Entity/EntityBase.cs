
namespace FastDev
{
    public abstract class EntityBase : IReference
    {
        public abstract int EntityID { get; }

        public abstract void OnInit(EntityData entityData);

        public abstract void OnUpdate();

        public abstract void OnClear();
    }
}
