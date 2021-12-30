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
        //object obj = ILRuntimeManager.appdomain.Instantiate("HotFixProject.Class7");

        var type = ILRuntimeManager.appdomain.GetType("HotFixProject.Class7");

        //热更DLL内的类型比较麻烦。首先我们得自己手动创建实例
        var ilInstance = new ILTypeInstance(type as ILType, false);//手动创建实例是因为默认方式会new MonoBehaviour，这在Unity里不允许
                                                                   //接下来创建Adapter实例
        var clrInstance = gameObject.AddComponent<UIPanelAdapter.Adapter>();
        //unity创建的实例并没有热更DLL里面的实例，所以需要手动赋值
        clrInstance.ILInstance = ilInstance;
        clrInstance.appdomain = ILRuntimeManager.appdomain;
        //这个实例默认创建的CLRInstance不是通过AddComponent出来的有效实例，所以得手动替换
        ilInstance.CLRInstance = clrInstance;


    }
}
