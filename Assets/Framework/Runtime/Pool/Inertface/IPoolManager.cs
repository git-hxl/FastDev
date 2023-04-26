using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Framework
{
    public interface IPoolManager
    {
        GameObject Allocate(string path);

        GameObject Allocate(string path, int autoRecycleTime = 0);

        void Recycle(GameObject obj);

        UniTaskVoid Recycle(GameObject obj, int millisecondsDelay);
    }
}
