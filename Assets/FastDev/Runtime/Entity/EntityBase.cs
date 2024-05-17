
using UnityEngine;

namespace FastDev
{
    public class EntityBase : ObjectBase
    {
        public int EntityID { get; private set; }
        public EntityData EntityData { get; private set; }

        public GameObject Object { get; private set; }

        public EntityBase() { }

        public virtual void Init(int id, string name, object target, EntityData entityData)
        {
            base.Init(name, target);

            EntityID = id;
            EntityData = entityData;

            Object = (GameObject)target;

            Object.name = $"{EntityID}";
        }

        public override void OnSpawn()
        {
            Debug.Log($"{EntityID} OnSpawn");
        }

        public override void OnUnspawn()
        {
            Debug.Log($"{EntityID} OnUnspawn");
        }

        public virtual void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {

        }


        public virtual void OnShow()
        {
            Object.SetActive(true);
            Debug.Log($"{EntityID} OnShow");

        }

        public virtual void OnHide()
        {
            Object.SetActive(false);
            Debug.Log($"{EntityID} OnHide");
        }




        public override void OnClear()
        {
            base.OnClear();

            if (Object != null)
            {
                GameObject.Destroy(Object);
            }

            Object = null;

            ReferencePool.Release(EntityData);

            EntityData = null;

            Debug.Log($"{EntityID} OnClear");
        }

    }
}