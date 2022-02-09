using FastDev;
using ILRuntime.CLR.TypeSystem;
using ILRuntime.Runtime.Intepreter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDemo : MonoBehaviour
{
    void Awake()
    {
        EventManager.Instance.Register(EventMsgID.OnHotFixInitSuccess, OnHotFixLoaded);
    }

    void OnHotFixLoaded(Hashtable hashtable)
    {
        var adapter = gameObject.AddComponent<UIPanelAdapter.Adapter>();
        ILTypeInstance iLTypeInstance = ILRuntimeManager.appdomain.Instantiate("HotFixProject.Class7");
        iLTypeInstance.CLRInstance = adapter;
        adapter.Init(ILRuntimeManager.appdomain, iLTypeInstance);
    }
}
