using Cysharp.Threading.Tasks;
using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                MsgManager.Instance.Enqueue(0, 101, 102);

                MsgManager.Instance.Dispatch(1, 11, 12);
            }
        });
    }

}