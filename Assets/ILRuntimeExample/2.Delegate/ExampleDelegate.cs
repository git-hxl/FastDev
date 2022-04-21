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
        object obj = ILRuntimeManager.instance.appdomain.Instantiate("Hotfix.Delegate");

        ILRuntimeManager.instance.appdomain.Invoke("Hotfix.Delegate", "Test", obj, null);

        Hashtable hashtable = new Hashtable();
        hashtable[0] = "Unity 111";
        MsgManager.instance.Dispatch(111, hashtable);
    }
}
