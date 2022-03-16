using FastDev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueTypeDemo : MonoBehaviour
{
    void Awake()
    {
        MsgManager.instance.Register(MsgID.OnHotFixInitSuccess, OnHotFixLoaded);
    }

    void OnHotFixLoaded(Hashtable hashtable)
    {
        object obj = ILRuntimeManager.instance.appdomain.Instantiate("HotFixProject.Class4");

        ILRuntimeManager.instance.appdomain.Invoke("HotFixProject.Class5", "Test", obj, null);

    }
}
