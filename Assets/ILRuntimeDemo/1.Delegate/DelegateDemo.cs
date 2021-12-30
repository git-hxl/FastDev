using Bigger;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DelegateDemo : MonoBehaviour
{
    public static Action<string> actionTest;
    public static UnityAction actionTest2;
    void Awake()
    {
        EventManager.Instance.Register(EventMsgID.OnHotFixInitSuccess, OnHotFixLoaded);
    }

    void OnHotFixLoaded(Hashtable hashtable)
    {
        object obj = ILRuntimeManager.appdomain.Instantiate("HotFixProject.Class2");

        ILRuntimeManager.appdomain.Invoke("HotFixProject.Class2", "Test", obj, null);

        actionTest.Invoke("hello");
        actionTest2.Invoke();
    }
}
