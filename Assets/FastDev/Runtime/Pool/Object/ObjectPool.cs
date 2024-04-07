using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.IO;

namespace FastDev
{
    public class ObjectPool : Singleton<ObjectPool>
    {
        public Dictionary<string, Stack<ObjectPoolComponent>> PoolObjects { get; private set; }
        public int MaxStack { get; } = 99;

        protected override void OnInit()
        {
            base.OnInit();
            PoolObjects = new Dictionary<string, Stack<ObjectPoolComponent>>();
        }

        private GameObject LoadAsset(string path)
        {
            var asset = AssetManager.Instance.LoadAsset<GameObject>("prefab", path);
            GameObject obj = GameObject.Instantiate(asset);
            return obj;
        }

        public GameObject Allocate(string path)
        {
            string key = Path.GetFileNameWithoutExtension(path);
            if (!PoolObjects.ContainsKey(key))
                PoolObjects[key] = new Stack<ObjectPoolComponent>();
            var stack = PoolObjects[key];

            ObjectPoolComponent poolObj = null;
            while (stack.Count > 0)
            {
                poolObj = stack.Pop();
                if (poolObj != null)
                {
                    break;
                }
            }
            if (poolObj == null)
            {
                var asset = LoadAsset(path);
                poolObj = asset.GetComponent<ObjectPoolComponent>();
                if (poolObj == null)
                    poolObj = asset.AddComponent<ObjectPoolComponent>();
                poolObj.Key = key;
            }
            poolObj.PoolState = PoolState.Allocated;
            poolObj.OnAllocated();
            return poolObj.gameObject;
        }

        /// <summary>
        /// 回收
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public void Recycle(GameObject obj)
        {
            if (!Application.isPlaying)
                return;
            if (obj == null)
                return;
            var poolObj = obj.GetComponent<ObjectPoolComponent>();
            if (poolObj == null)
                return;
            if (poolObj.PoolState == PoolState.Allocated || poolObj.PoolState == PoolState.WaitToRecycled)
            {
                string key = poolObj.Key;
                if (!PoolObjects.ContainsKey(key))
                    PoolObjects[key] = new Stack<ObjectPoolComponent>();
                var stack = PoolObjects[key];
                if (stack.Count < MaxStack && !stack.Contains(poolObj))
                {
                    stack.Push(poolObj);
                    obj.SetActive(false);
                    poolObj.PoolState = PoolState.Recycled;
                    poolObj.OnRecycled();
                }
                else
                {
                    GameObject.Destroy(obj);
                }
            }
        }

        /// <summary>
        /// 延迟回收
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="millisecondsDelay"></param>
        /// <returns></returns>
        public async UniTaskVoid Recycle(GameObject obj, int millisecondsDelay)
        {
            if (obj == null)
                return;

            var poolObj = obj.GetComponent<ObjectPoolComponent>();

            if (poolObj == null) return;

            poolObj.PoolState = PoolState.WaitToRecycled;

            await UniTask.Delay(millisecondsDelay);

            if (poolObj.PoolState == PoolState.WaitToRecycled)
            {
                Recycle(obj);
            }
        }

    }
}