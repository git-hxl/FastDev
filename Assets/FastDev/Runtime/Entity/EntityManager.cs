using System.Collections.Generic;
using UnityEngine;

namespace FastDev
{
    /// <summary>
    /// 实体管理器
    /// </summary>
    public sealed partial class EntityManager : MonoSingleton<EntityManager>
    {
        private readonly Dictionary<int, EntityBase> m_Entities;

        private readonly List<EntityBase> m_EntitiesList;
        private int serialId;

        public EntityManager()
        {
            m_Entities = new Dictionary<int, EntityBase>();
            m_EntitiesList = new List<EntityBase>();
            serialId = 0;
        }

        public T ShowEntity<T>(string entityAssetName, EntityData entityData) where T : EntityBase, new()
        {
            var objectPool = ObjectPoolManager.Instance.GetObjectPool<T>(entityAssetName);

            if (objectPool == null)
            {
                objectPool = ObjectPoolManager.Instance.CreateSpawnObjectPool<T>(entityAssetName, 15, 50, 60);
            }

            T t = objectPool.Spawn();

            if (t == null)
            {
                t = ReferencePool.Acquire<T>();

                var objectAsset = ResourceManager.Instance.LoadAsset<GameObject>("prefab", entityAssetName);

                var gameObject = GameObject.Instantiate(objectAsset);

                serialId++;
                t.Init(serialId, entityAssetName, gameObject);
                objectPool.Register(t, true);
                m_Entities.Add(serialId, t);
            }

            t.OnShow(entityData);

            m_EntitiesList.Add(t);

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

        public void RemoveEntity(int entityID)
        {
            if (m_Entities.ContainsKey(entityID))
            {
                m_Entities.Remove(entityID);
            }

        }

        public void HideEntity<T>(int entityID) where T : EntityBase
        {
            if (m_Entities.ContainsKey(entityID))
            {
                var entity = m_Entities[entityID];
                var objectPool = ObjectPoolManager.Instance.GetObjectPool<T>(entity.AssetName);
                objectPool.Unspawn(entity as T);
                m_EntitiesList.Remove(entity);
            }
        }


        private void Update()
        {
            for (int i = m_EntitiesList.Count - 1; i >= 0; i--)
            {
                m_EntitiesList[i].OnUpdate();
            }
        }

        private void LateUpdate()
        {
            for (int i = m_EntitiesList.Count - 1; i >= 0; i--)
            {
                m_EntitiesList[i].OnLateUpdate();
            }
        }

        private void FixedUpdate()
        {
            for (int i = m_EntitiesList.Count - 1; i >= 0; i--)
            {
                m_EntitiesList[i].OnFixedUpdate();
            }
        }

        private void OnDestroy()
        {
            m_Entities.Clear();

            m_EntitiesList.Clear();
        }


    }
}