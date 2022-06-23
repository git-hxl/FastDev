using FastDev;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExampleDelegate : MonoBehaviour
{
    void Start()
    {
        OnHotFixLoaded();
    }

    void OnHotFixLoaded()
    {
        object obj = ILRuntimeManager.Instance.appdomain.Instantiate("Hotfix.Delegate");

        ILRuntimeManager.Instance.appdomain.Invoke("Hotfix.Delegate", "Test", obj, null);

        Hashtable hashtable = new Hashtable();
        hashtable[0] = "Unity 111";
        MsgManager.Instance.Dispatch(111, hashtable);
    }
}
