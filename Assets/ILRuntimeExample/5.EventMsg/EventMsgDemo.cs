using FastDev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMsgDemo : MonoBehaviour
{
    void Awake()
    {
        MsgManager.Instance.Register(MsgID.OnHotFixInitSuccess, OnHotFixLoaded);
    }

    void OnHotFixLoaded(Hashtable hashtable)
    {
        object obj = ILRuntimeManager.Instance.appdomain.Instantiate("HotFixProject.Class6");

        ILRuntimeManager.Instance.appdomain.Invoke("HotFixProject.Class6", "Test", obj, null);

        Hashtable hashtable1 = new Hashtable();
        hashtable1.Add(0, "hello");
        MsgManager.Instance.Dispatch(-1, hashtable1);

    }
}
