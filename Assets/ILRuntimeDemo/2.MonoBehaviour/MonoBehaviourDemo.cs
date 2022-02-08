using FastDev;
using ILRuntime.CLR.TypeSystem;
using ILRuntime.Runtime.Intepreter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourDemo : MonoBehaviour
{
    void Awake()
    {
        EventManager.Instance.Register(EventMsgID.OnHotFixInitSuccess, OnHotFixLoaded);
    }

    void OnHotFixLoaded(Hashtable hashtable)
    {
        //object obj = ILRuntimeManager.appdomain.Instantiate("HotFixProject.Class3");

        ILRuntimeManager.appdomain.Invoke("HotFixProject.Class3", "Add", null, gameObject);
    }

}
