using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FastDev;
public class ExampleInvoke : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        OnHotFixLoaded();
    }

    void OnHotFixLoaded()
    {
        //调用静态方法
        ILRuntimeManager.Instance.appdomain.Invoke("Hotfix.Invoke", "Test1", null, null);

        //调用实列方法
        object class1 = ILRuntimeManager.Instance.appdomain.Instantiate("Hotfix.Invoke");

        object s=  ILRuntimeManager.Instance.appdomain.Invoke("Hotfix.Invoke", "Test2", class1, null);
        //输出返回值
        Debug.Log(s);

        //传参
        ILRuntimeManager.Instance.appdomain.Invoke("Hotfix.Invoke", "Test3", class1, "Unity");

    }
}
