using Bigger;
using ILRuntime.CLR.TypeSystem;
using ILRuntime.Runtime.Intepreter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourDemo : MonoBehaviour
{
    void Awake()
    {
        EventManager.Instance.Register(MsgID.OnHotFixInitSuccess, OnHotFixLoaded);
    }

    void OnHotFixLoaded(Hashtable hashtable)
    {
        object obj = ILRuntimeManager.appdomain.Instantiate("HotFixProject.Class2");

        ILRuntimeManager.appdomain.Invoke("HotFixProject.Class2", "Test", obj, null);

        IType type = ILRuntimeManager.appdomain.LoadedTypes["HotFixProject.Class3"];

        //热更DLL内的类型比较麻烦。首先我们得自己手动创建实例
        var ilInstance = new ILTypeInstance(type as ILType, false);//手动创建实例是因为默认方式会new MonoBehaviour，这在Unity里不允许
                                                                   //接下来创建Adapter实例
        var clrInstance = gameObject.AddComponent<MonoBehaviourAdapter.Adaptor>();
        //unity创建的实例并没有热更DLL里面的实例，所以需要手动赋值
        clrInstance.ILInstance = ilInstance;
        clrInstance.AppDomain = ILRuntimeManager.appdomain;
        //这个实例默认创建的CLRInstance不是通过AddComponent出来的有效实例，所以得手动替换
        ilInstance.CLRInstance = clrInstance;

        //res = clrInstance.ILInstance;//交给ILRuntime的实例应该为ILInstance

        clrInstance.Awake();//因为Unity调用这个方法时还没准备好所以这里补调一次
    }


    MonoBehaviourAdapter.Adaptor GetComponent(ILType type)
    {
        var arr = GetComponents<MonoBehaviourAdapter.Adaptor>();
        for (int i = 0; i < arr.Length; i++)
        {
            var instance = arr[i];
            if (instance.ILInstance != null && instance.ILInstance.Type == type)
            {
                return instance;
            }
        }
        return null;
    }
}
