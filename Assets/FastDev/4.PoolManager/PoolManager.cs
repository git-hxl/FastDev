using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.IO;

namespace FastDev
{
    public class PoolManager : MonoSingleton<PoolManager>, IPoolManager
    {
        private string objTag = "(Pool)";
        public Dictionary<string, Stack<GameObject>> PoolObjects { get; } = new Dictionary<string, Stack<GameObject>>();
        public int MaxStack { get; } = 99;

        GameObject IPoolManager.LoadAsset(string path)
        {
            GameObject obj = Instantiate(AssetManager.Instance.LoadAsset<GameObject>("prefab", path));
            obj.name = Path.GetFileNameWithoutExtension(path) + objTag;
            return obj;
        }

        public GameObject Allocate(string path)
        {
            string name = Path.GetFileNameWithoutExtension(path) + objTag;
            if (!PoolObjects.ContainsKey(name))
                PoolObjects[name] = new Stack<GameObject>();
            var stack = PoolObjects[name];

            GameObject obj = null;
            while (stack.Count > 0)
            {
                obj = stack.Pop();
                if (obj != null)
                {
                    break;
                }
            }
            if (obj == null)
                obj = (this as IPoolManager).LoadAsset(path);
            return obj;
        }

        public GameObject Allocate(string path, int autoRecycleTime = 0)
        {
            GameObject obj = Allocate(path);

            if (autoRecycleTime > 0)
            {
                Recycle(obj, autoRecycleTime).Forget();
            }
            return obj;
        }

        /// <summary>
        /// 回收
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public void Recycle(GameObject obj)
        {
            if (obj == null)
                return;
            string objName = obj.name;
            if (!objName.Contains(objTag))
                return;
            if (!PoolObjects.ContainsKey(objName))
                PoolObjects[objName] = new Stack<GameObject>();
            var stack = PoolObjects[objName];
            if (stack.Count < MaxStack && !stack.Contains(obj))
            {
                stack.Push(obj);
                obj.SetActive(false);
                return;
            }
            Destroy(obj.gameObject);
        }

        /// <summary>
        /// 延迟回收
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="millisecondsDelay"></param>
        /// <returns></returns>
        public async UniTaskVoid Recycle(GameObject obj, int millisecondsDelay)
        {
            await UniTask.Delay(millisecondsDelay);
            Recycle(obj);
        }

    }
}