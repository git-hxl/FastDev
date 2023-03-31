using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.IO;

namespace FastDev
{
    public class PoolManager : MonoSingleton<PoolManager>
    {
        public Dictionary<string, Stack<PoolObject>> PoolObjects { get; } = new Dictionary<string, Stack<PoolObject>>();
        public int MaxStack { get; } = 9;

        public GameObject PoolParent;

        protected override void OnInit()
        {
            base.OnInit();

            PoolParent = new GameObject("PoolParent");
        }

        private GameObject LoadAsset(string path)
        {
            var asset = AssetManager.Instance.LoadAsset<GameObject>("prefab", path);
            GameObject obj = Instantiate(asset, PoolParent.transform);
            obj.name = Path.GetFileNameWithoutExtension(path);
            return obj;
        }

        public GameObject Allocate(string path)
        {
            string name = Path.GetFileNameWithoutExtension(path);
            if (!PoolObjects.ContainsKey(name))
                PoolObjects[name] = new Stack<PoolObject>();
            var stack = PoolObjects[name];

            PoolObject poolObj = null;
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
                poolObj = asset.GetComponent<PoolObject>();
                if (poolObj == null)
                    poolObj = asset.AddComponent<PoolObject>();
            }
            poolObj.PoolState = PoolState.Allocated;
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
            if (PoolParent == null)
                return;
            if (obj == null)
                return;
            var poolObj = obj.GetComponent<PoolObject>();
            if (poolObj == null)
                return;
            if (poolObj.PoolState == PoolState.Allocated || poolObj.PoolState == PoolState.WaitToRecycled)
            {
                string objName = obj.name;
                if (!PoolObjects.ContainsKey(objName))
                    PoolObjects[objName] = new Stack<PoolObject>();
                var stack = PoolObjects[objName];
                if (stack.Count < MaxStack && !stack.Contains(poolObj))
                {
                    stack.Push(poolObj);
                    obj.SetActive(false);
                    obj.transform.SetParent(PoolParent.transform);
                    poolObj.PoolState = PoolState.Recycled;
                }
                else
                {
                    Destroy(obj);
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

            var poolObj = obj.GetComponent<PoolObject>();

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