using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FastDev;
public class HelloWorldDemo : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        MsgManager.instance.Register(MsgID.OnHotFixInitSuccess, OnHotFixLoaded);
    }

    void OnHotFixLoaded(Hashtable hashtable)
    {
        //HelloWorld，第一次方法调用
        ILRuntimeManager.instance.appdomain.Invoke("HotFixProject.Class1", "Test1", null, null);

        object class1 = ILRuntimeManager.instance.appdomain.Instantiate("HotFixProject.Class1");

        ILRuntimeManager.instance.appdomain.Invoke("HotFixProject.Class1", "Test2", class1, null);

        ILRuntimeManager.instance.appdomain.Invoke("HotFixProject.Class1", "Test3", class1, null);

    }
}
