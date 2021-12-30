using Bigger;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMsgDemo : MonoBehaviour
{
    void Awake()
    {
        EventManager.Instance.Register(EventMsgID.OnHotFixInitSuccess, OnHotFixLoaded);
    }

    void OnHotFixLoaded(Hashtable hashtable)
    {
        object obj = ILRuntimeManager.appdomain.Instantiate("HotFixProject.Class6");

        ILRuntimeManager.appdomain.Invoke("HotFixProject.Class6", "Test", obj, null);

        Hashtable hashtable1 = new Hashtable();
        hashtable1.Add(0, "hello");
        EventManager.Instance.Dispatch(-1, hashtable1);

    }
}
