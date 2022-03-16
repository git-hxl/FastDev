using FastDev;
using FastDev.UI;
using ILRuntime.CLR.TypeSystem;
using ILRuntime.Runtime.Intepreter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDemo : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            UIManager.instance.OpenUI("Assets/ILRuntimeExample/6.UI/HotFixUI.prefab");
        }
    }
}
