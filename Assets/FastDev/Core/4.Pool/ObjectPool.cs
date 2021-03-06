using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
namespace FastDev
{
    public class ObjectPool : MonoSingleton<ObjectPool>
    {
        private string bundleName = "prefab";
        private string objTag = "(Pool)";
        private Dictionary<string, Stack<GameObject>> poolObjects = new Dictionary<string, Stack<GameObject>>();

        public int maxPoolNum = 99;
        /// <summary>
        /// 分配
        /// </summary>
        /// <param name="bundleName"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public GameObject Allocate(string path)
        {
            string objName = path.GetFileNameWithoutExtension() + objTag;
            if (!poolObjects.ContainsKey(objName))
                poolObjects[objName] = new Stack<GameObject>();
            var stack = poolObjects[objName];
            while (stack.Count > 0)
            {
                GameObject poolObj = stack.Pop();
                if (poolObj != null)
                {
                    poolObj.SetActive(true);
                    return poolObj;
                }
            }
            GameObject obj = Instantiate(ResLoader.Instance.LoadAsset<GameObject>(bundleName, path));
            obj.name = objName;
            return obj;
        }
        /// <summary>
        /// 分配多个
        /// </summary>
        /// <param name="bundleName"></param>
        /// <param name="path"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public GameObject[] Allocate(string path, int count)
        {
            GameObject[] poolObjects = new GameObject[count];

            for (int i = 0; i < count; i++)
            {
                poolObjects[i] = Allocate(path);
            }
            return poolObjects;
        }
        /// <summary>
        /// 回收
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Recycle(GameObject obj)
        {
            if (obj == null)
                return false;
            string objName = obj.name;
            if (!objName.Contains(objTag))
                return false;
            if (!poolObjects.ContainsKey(objName))
                poolObjects[objName] = new Stack<GameObject>();
            var stack = poolObjects[objName];
            if (stack.Count < maxPoolNum && !stack.Contains(obj))
            {
                obj.SetActive(false);
                stack.Push(obj);
                return true;
            }
            Destroy(obj);
            return false;
        }
        /// <summary>
        /// 回收多个
        /// </summary>
        /// <param name="objs"></param>
        public bool Recycle(GameObject[] objs)
        {
            foreach (var item in objs)
            {
                Recycle(item);
            }
            return true;
        }
        /// <summary>
        /// 延迟回收
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="millisecondsDelay"></param>
        /// <returns></returns>
        public async UniTask<bool> Recycle(GameObject obj, int millisecondsDelay)
        {
            await UniTask.Delay(millisecondsDelay);
            return Recycle(obj);
        }
        public async UniTask<bool> Recycle(GameObject[] objs, int millisecondsDelay)
        {
            await UniTask.Delay(millisecondsDelay);
            return Recycle(objs);
        }
        /// <summary>
        /// 清理缓存
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
            foreach (var item in poolObjects)
            {
                foreach (var obj in item.Value)
                {
                    Destroy(obj);
                }
            }
            poolObjects.Clear();
            Resources.UnloadUnusedAssets();
        }
    }
}