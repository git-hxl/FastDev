using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using FastDev;
using UnityEngine;

public class Send1 : MonoBehaviour
{
    Thread thread1;
    Thread thread2;
    private void Start()
    {
        Hashtable hashtable = new Hashtable();
        hashtable.Add(0, "Hello");
        MsgManager.Instance.Dispatch(111, hashtable);

        thread1 = new Thread(() =>
        {
            while(true)
            {
                Thread.Sleep(1000);
                MsgManager.Instance.Enqueue(222, hashtable);
            }
        });
        thread1.Start();
    }

    float time = 0f;
    private void Update()
    {
        time += Time.deltaTime;
        if (time > 1)
        {
            Hashtable hashtable = new Hashtable();
            hashtable.Add(0, "World");
            MsgManager.Instance.Dispatch(111, hashtable);
            MsgManager.Instance.Dispatch(333, hashtable);
            hashtable[0] = "Hello";
            time = 0;
        }
    }

    private void OnDestroy()
    {
        thread1.Abort();
    }

    
    [ContextMenu("GC")]
    private void GC()
    {
        System.GC.Collect();
    }

}
