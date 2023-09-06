using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace GameFramework
{
    public class PoolObject : MonoBehaviour
    {
        public int AutoDestroy = 10;
        public string Key { get; set; }
        public PoolState PoolState { get; set; }

        private CancellationTokenSource cancellationToken;

        public void OnAllocated()
        {
            if (cancellationToken != null)
                cancellationToken.Cancel();
        }

        public async void OnRecycled()
        {
            //超过一定时间 自动释放
            cancellationToken = new CancellationTokenSource();
            bool isCanceled = await UniTask.Delay(AutoDestroy * 1000, cancellationToken: cancellationToken.Token).SuppressCancellationThrow();
            if (!isCanceled)
                Destroy(gameObject);
        }

    }
}
