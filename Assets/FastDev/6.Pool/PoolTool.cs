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
            poolNum = Mathf.Clamp(poolNum, 0, PoolManager.Instance.maxPoolNum);
            GameObject[] objs = PoolManager.Instance.Allocate(assetPath, poolNum);
            PoolManager.Instance.Recycle(objs);
        }
    }
}