using UnityEngine;
using Cysharp.Threading.Tasks;
namespace FastDev
{
    public class PoolTool : MonoBehaviour
    {
        public string assetPath;
        public int poolNum;
        public int delay;
        private async UniTaskVoid Start()
        {
            await UniTask.Delay(delay);
            poolNum = Mathf.Clamp(poolNum, 0, ObjectPool.Instance.maxPoolNum);
            GameObject[] objs = ObjectPool.Instance.Allocate(assetPath, poolNum);
            ObjectPool.Instance.Recycle(objs);
        }
    }
}