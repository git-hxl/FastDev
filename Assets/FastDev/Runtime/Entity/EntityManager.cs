using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System.Collections.Generic;
using UnityEngine;

namespace FastDev
{
    /// <summary>
    /// 实体管理器
    /// </summary>
    public sealed partial class EntityManager : GameModule
    {
        private readonly Dictionary<int, EntityBase> m_Entities;
        private int serialId;

        public EntityManager()
        {
            m_Entities = new Dictionary<int, EntityBase>();
            serialId = 0;
        }

        public T ShowEntity<T>(string entityAssetName, EntityData entityData) where T : EntityBase, new()
        {
            var objectPool = GameEntry.ObjectPool.GetObjectPool<T>(entityAssetName);

            if (objectPool == null)
            {
                objectPool = GameEntry.ObjectPool.CreateSpawnObjectPool<T>(entityAssetName, 15, 50, 60);
            }

            T t = objectPool.Spawn();

            if (t == null)
            {
                t = ReferencePool.Acquire<T>();

                var objectAsset = GameEntry.Resource.LoadAsset<GameObject>("prefab", entityAssetName);

                var gameObject = GameObject.Instantiate(objectAsset);

                serialId++;
                t.Init(serialId, entityAssetName, gameObject, entityData);
                m_Entities.Add(serialId, t);

                objectPool.Register(t, true);

                t.OnShow();
            }

            return t;
        }


        public T GetEntity<T>(int entityID) where T : EntityBase
        {
            if (m_Entities.ContainsKey(entityID))
            {
                var entity = m_Entities[entityID];
                return entity as T;
            }
            return null;
        }

        public void HideEntity<T>(int entityID) where T: EntityBase
        {
            if (m_Entities.ContainsKey(entityID))
            {
                var entity = m_Entities[entityID];
                entity.OnHide();

                var objectPool = GameEntry.ObjectPool.GetObjectPool<T>(entity.Name);
                objectPool.Unspawn(entity as T);

                m_Entities.Remove(entityID);
            }
        }

        internal override void Shutdown()
        {
            m_Entities.Clear();
        }

        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
            foreach (var item in m_Entities)
            {
                item.Value.OnUpdate(elapseSeconds, realElapseSeconds);
            }
        }
    }
}