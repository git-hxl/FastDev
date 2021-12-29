using Bigger;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DelegateDemo : MonoBehaviour
{
    public static Action<string> actionTest;
    void Awake()
    {
        EventManager.Instance.Register(MsgID.OnHotFixInitSuccess, OnHotFixLoaded);
    }

    void OnHotFixLoaded(Hashtable hashtable)
    {
        object obj = ILRuntimeManager.appdomain.Instantiate("HotFixProject.Class2");

        ILRuntimeManager.appdomain.Invoke("HotFixProject.Class2", "Test", obj, null);

        actionTest.Invoke("hello");
    }
}
