using FastDev;
using Google.Protobuf;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoDemo : MonoBehaviour
{
    void Awake()
    {
        MsgManager.instance.Register(MsgID.OnHotFixInitSuccess, OnHotFixLoaded);
    }

    void OnHotFixLoaded(Hashtable hashtable)
    {
        object class1 = ILRuntimeManager.instance.appdomain.Instantiate("HotFixProject.Class8");

        ILRuntimeManager.instance.appdomain.Invoke("HotFixProject.Class8","Test", class1);
        Student student = new Student();
        student.Name = "Unity:asdad";
        ILRuntimeManager.instance.appdomain.Invoke("HotFixProject.Class8", "Test2", class1,student.ToByteArray());
    }
}
