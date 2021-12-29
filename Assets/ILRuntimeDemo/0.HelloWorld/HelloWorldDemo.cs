using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bigger;
public class HelloWorldDemo : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        EventManager.Instance.Register(MsgID.OnHotFixInitSuccess, OnHotFixLoaded);
    }

    void OnHotFixLoaded(Hashtable hashtable)
    {
        //HelloWorld，第一次方法调用
        ILRuntimeManager.appdomain.Invoke("HotFixProject.Class1", "Test1", null, null);

        object class1 = ILRuntimeManager.appdomain.Instantiate("HotFixProject.Class1");

        ILRuntimeManager.appdomain.Invoke("HotFixProject.Class1", "Test2", class1, null);

    }
}
