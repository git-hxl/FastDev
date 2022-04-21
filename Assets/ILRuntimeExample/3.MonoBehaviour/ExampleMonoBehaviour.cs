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
        MsgManager.instance.Register(MsgID.OnHotFixInitSuccess, OnHotFixLoaded);

        MsgManager.instance.Register(666, OnAddOk);
    }

    void OnHotFixLoaded(Hashtable hashtable)
    {
        //object obj = ILRuntimeManager.appdomain.Instantiate("HotFixProject.Class3");

        ILRuntimeManager.instance.appdomain.Invoke("HotFixProject.Class3", "Add", null, gameObject);
    }
    

    void OnAddOk(Hashtable hashtable)
    {
        Debug.Log("Unity AddOk:"+hashtable["xx"]);
    }
}
