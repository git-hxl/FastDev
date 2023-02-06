
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace FastDev
{
    public interface IPoolManager
    {
        GameObject LoadAsset(string path);
        GameObject Allocate(string path);

        GameObject Allocate(string path, int autoRecycleTime = 0);

        void Recycle(GameObject obj);
        UniTaskVoid Recycle(GameObject obj, int millisecondsDelay);
    }
}
