using Bigger;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void TestDelegate1(string a);
public class DelegateDemo : MonoBehaviour
{
    public static TestDelegate1 testDelegatefunc;
    void Awake()
    {
        EventManager.Instance.Register(MsgID.OnHotFixInitSuccess, OnHotFixLoaded);

        testDelegatefunc += test;
    }

    void OnHotFixLoaded(Hashtable hashtable)
    {
        object class1 = ILRuntimeManager.appdomain.Instantiate("HotFixProject.Class2");

        ILRuntimeManager.appdomain.Invoke("HotFixProject.Class2", "Init", class1, null);

    }

    public void test(string a)
    { Debug.Log("unity delegate test:" + a); }
}
