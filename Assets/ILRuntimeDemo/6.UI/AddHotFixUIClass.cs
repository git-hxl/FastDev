using FastDev;
using ILRuntime.CLR.TypeSystem;
using ILRuntime.Runtime.Intepreter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHotFixUIClass : MonoBehaviour
{
    public string fullClassName;
    private void Awake()
    {
        var adapter = gameObject.AddComponent<UIPanelAdapter.Adapter>();
        IType itype = ILRuntimeManager.appdomain.GetType(fullClassName);
        ILTypeInstance iLTypeInstance = new ILTypeInstance(itype as ILType, false);
        iLTypeInstance.CLRInstance = adapter;
        adapter.Init(ILRuntimeManager.appdomain, iLTypeInstance);
    }
}
