using HybridCLR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
public class LoadDll : MonoBehaviour
{

    void Start()
    {
        // In the Editor environment, HotUpdate.dll.bytes has been automatically loaded and does not need to be loaded. Repeated loading will cause problems.
#if !UNITY_EDITOR
         Assembly hotUpdateAss = Assembly.Load(File.ReadAllBytes($"{Application.streamingAssetsPath}/HotUpdate.dll.bytes"));
#else
        // No need to load under Editor, directly find the HotUpdate assembly
        Assembly hotUpdateAss = System.AppDomain.CurrentDomain.GetAssemblies().First(a => a.GetName().Name == "HotUpdate");
#endif

        Type type = hotUpdateAss.GetType("Hello");
        type.GetMethod("Run").Invoke(null, null);
    }
}
