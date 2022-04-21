using FastDev;
using ILRuntime.CLR.TypeSystem;
using ILRuntime.Runtime.Intepreter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleMonoBehaviour : MonoBehaviour
{
    void Start()
    {

        ILRuntimeManager.instance.appdomain.Invoke("Hotfix.HotfixMono", "Add", null, gameObject);
    }
    

}
