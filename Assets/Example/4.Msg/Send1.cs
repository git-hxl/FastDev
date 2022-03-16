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
        MsgManager.instance.Dispatch(111, hashtable);
    }

    float time = 0f;
    private void Update()
    {
        time += Time.deltaTime;
        //if (time > 1)
        {
            Hashtable hashtable = new Hashtable();
            hashtable.Add(0, "World");
            MsgManager.instance.Enqueue(222,hashtable);
            time = 0;
        }
    }
}
