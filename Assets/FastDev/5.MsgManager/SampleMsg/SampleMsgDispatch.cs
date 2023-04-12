using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FastDev
{
    public class SampleMsgDispatch : MonoBehaviour
    {
        private void Start()
        {
            UniTask.Create(async () =>
            {
                while (true)
                {
                    await UniTask.Delay(1000);
                    //同步线程消息
                    MsgSyncManager.Instance.Enqueue(0, new byte[] { 1 });

                    MsgManager.Instance.Dispatch(0, 100);
                }
            });
        }

    }
}
