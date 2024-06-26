
using UnityEngine;

namespace FastDev
{
    public class EntityBase : ObjectBase
    {
        public int EntityID { get; private set; }
        public EntityData EntityData { get; private set; }

        public GameObject Object { get; private set; }

        public EntityBase() { }

        public virtual void Init(int id, string name, object target)
        {
            base.Init(name, target);

            EntityID = id;

            Object = (GameObject)target;

            Object.name = $"{Object.name}_{EntityID}";
        }

        public override void OnSpawn()
        {
            Debug.Log($"{EntityID} OnSpawn");
        }

        public virtual void OnShow(EntityData entityData)
        {
            EntityData = entityData;
            Debug.Log($"{EntityID} OnShow");
        }


        public override void OnUnspawn()
        {
            Debug.Log($"{EntityID} OnUnspawn");

            if (EntityData != null)
            {
                ReferencePool.Release(EntityData);

                EntityData = null;
            }
        }

        public virtual void OnUpdate()
        {

        }

        public virtual void OnLateUpdate()
        {

        }

        public virtual void OnFixedUpdate()
        {

        }

        public override void OnClear()
        {
            base.OnClear();

            if (Object != null)
            {
                GameObject.Destroy(Object);
            }

            Object = null;


            Debug.Log($"{EntityID} OnClear");
        }
    }
}