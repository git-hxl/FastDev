using FastDev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMsgDemo : MonoBehaviour
{
    void Awake()
    {
        MsgManager.instance.Register(MsgID.OnHotFixInitSuccess, OnHotFixLoaded);
    }

    void OnHotFixLoaded(Hashtable hashtable)
    {
        object obj = ILRuntimeManager.instance.appdomain.Instantiate("HotFixProject.Class6");

        ILRuntimeManager.instance.appdomain.Invoke("HotFixProject.Class6", "Test", obj, null);

        Hashtable hashtable1 = new Hashtable();
        hashtable1.Add(0, "hello");
        MsgManager.instance.Dispatch(-1, hashtable1);

    }
}
