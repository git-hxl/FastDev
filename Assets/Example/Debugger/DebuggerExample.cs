using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DebuggerExample : MonoBehaviour
{
    public class TestA
    {
        public string condition;
        public string stackTrace;
        public LogType type;
    }

    List<TestA> testListA = new List<TestA>();

    //async UniTaskVoid Start()
    //{
    //Application.logMessageReceived += Application_logMessageReceived;

    //int[] test = new int[1];

    //Debug.Log(Thread.CurrentThread.ManagedThreadId);
    //await UniTask.Run(() =>
    //{
    //Debug.Log(Thread.CurrentThread.ManagedThreadId);
    //var v = test[1];
    //});

    //}
    Thread thread;
    private void Start()
    {
        thread = new Thread(() => {
            Thread.Sleep(200);
            int[] o = new int[1];
            var i = o[1];
        });
        thread.Start();
    }

    float countTime = 0.01f;
    // Update is called once per frame
    void Update()
    {
        countTime -= Time.deltaTime;
        if (countTime <= 0)
        {
            long timeStamp = Bigger.TimeUtil.GetCurTimestamp();
            string time = Bigger.TimeUtil.TimestampToDateTime(timeStamp).ToString();
            Debug.Log(timeStamp);
            countTime = 0.2f;
        }
    }

    [ContextMenu("GC")]
    void GC()
    {
        System.GC.Collect();
    }

    private void OnDestroy()
    {
        if (thread != null)
            thread.Abort();
    }       
}
