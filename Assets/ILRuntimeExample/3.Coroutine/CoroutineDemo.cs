using FastDev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineDemo : MonoBehaviour
{
    void Awake()
    {
        MsgManager.Instance.Register(MsgID.OnHotFixInitSuccess, OnHotFixLoaded);
    }

    void OnHotFixLoaded(Hashtable hashtable)
    {
        object obj = ILRuntimeManager.appdomain.Instantiate("HotFixProject.Class4");

        ILRuntimeManager.appdomain.Invoke("HotFixProject.Class4", "Test", obj, this);
         
    }
}
