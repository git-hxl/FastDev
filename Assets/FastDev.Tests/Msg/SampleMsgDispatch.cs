using Cysharp.Threading.Tasks;
using FastDev;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SampleMsgDispatch : MonoBehaviour
{
    Thread thread;
    private void Start()
    {
        MessageManager<string>.Instance.Dispatch(MsgID.TestID, "11");

        thread = new Thread(() =>
        {
            while (true)
            {
                Thread.Sleep(1000);
                MsgManagerQuene<int[]>.Instance.Enqueue(MsgID.TestID, new int[] { 22, 33, 44 });

                MessageManager<string>.Instance.Dispatch(MsgID.TestID, "22");
            }
        })
        {

        };
        thread.Start();
    }

    private void OnDestroy()
    {
        thread.Abort();
    }
}