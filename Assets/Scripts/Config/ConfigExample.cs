using FastDev;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class ConfigExample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string[] files = Directory.GetFiles(Application.streamingAssetsPath + "/JsonConfig").Where((a)=>a.EndsWith("json")).ToArray();

        ConfigManager.Instance.Init(files);

        Test1 test1 = ConfigManager.Instance.GetConfig<Test1>(1001);
        Debug.Log(test1.Name);
        test1 = ConfigManager.Instance.GetConfig<Test1>(1002);
        Debug.Log(test1.Name);
        test1 = ConfigManager.Instance.GetConfig<Test1>(1003);
        Debug.Log(test1.Name);

        Test2 test2 = ConfigManager.Instance.GetConfig<Test2>(1001);
        Debug.Log(test2.Name);
        test2 = ConfigManager.Instance.GetConfig<Test2>(1002);
        Debug.Log(test2.Name);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
