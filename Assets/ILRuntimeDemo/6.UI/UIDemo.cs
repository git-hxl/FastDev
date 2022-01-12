using Bigger;
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
        ILRuntimeManager.appdomain.Invoke("HotFixProject.Class7", "Add", null, gameObject);
    }
}
