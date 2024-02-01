using FastDev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class SampleMsg : MonoBehaviour
{
    //SampleMsg2 sampleMsg2;
    // Start is called before the first frame update
    void Start()
    {
        MsgManager<string>.Instance.Register(1, Test);

        SampleMsg2 sampleMsg2 = new SampleMsg2();

        MsgManager<string[]>.Instance.Register(1, sampleMsg2.Test_SampleMsg2);
    }

    private void Test(string args)
    {
        Debug.Log("SampleMsg: " + args);
    }

}


public class SampleMsg2
{
    ~SampleMsg2()
    {
        Debug.LogError("SampleMsg2 析构执行");
    }

    public void Test_SampleMsg2(string[] args)
    {
        foreach (var item in args)
        {
            Debug.Log("SampleMsg2: " + item);
        }
    }
}