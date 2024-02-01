using Cysharp.Threading.Tasks;
using FastDev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleMsgDispatch : MonoBehaviour
{
    private void Start()
    {
        MsgManager<string>.Instance.Dispatch(1, "11");
        UniTask.Create(async () =>
        {
            while (true)
            {
                await UniTask.Delay(1000);
                //同步线程消息
               // MsgManager<string[]>.Instance.Enqueue(0, new string[] { "111", "2222" });

                MsgManager<string>.Instance.Dispatch(1, "11");
            }
        });
    }

}