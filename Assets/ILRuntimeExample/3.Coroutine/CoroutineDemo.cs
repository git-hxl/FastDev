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
        object obj = ILRuntimeManager.Instance.appdomain.Instantiate("HotFixProject.Class4");

        ILRuntimeManager.Instance.appdomain.Invoke("HotFixProject.Class4", "Test", obj, this);
         
    }
}
