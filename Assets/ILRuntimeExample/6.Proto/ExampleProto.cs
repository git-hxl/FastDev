using FastDev;
using Google.Protobuf;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleProto : MonoBehaviour
{
    void Start()
    {
        object instance = ILRuntimeManager.instance.appdomain.Instantiate("Hotfix.Proto");
        Student student = new Student();
        student.Name = "Unity:asdad";
        ILRuntimeManager.instance.appdomain.Invoke("Hotfix.Proto", "Test", instance, student.ToByteArray());
    }
}
