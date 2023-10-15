using FastDev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleMsg : MonoBehaviour
{
    //SampleMsg2 sampleMsg2;
    // Start is called before the first frame update
    void Start()
    {
        MsgManager.Instance.Register(0, Test);

        SampleMsg2 sampleMsg2 = new SampleMsg2();

        MsgManager.Instance.Register(1, sampleMsg2.Test_SampleMsg2);
    }

    private void Test(object[] args)
    {
        foreach (var item in args)
        {
            Debug.Log("SampleMsg: " + item.ToString());
        }

    }

}


public class SampleMsg2
{
    ~SampleMsg2()
    {
        Debug.LogError("SampleMsg2 析构执行");
    }

    public void Test_SampleMsg2(object[] args)
    {
        foreach (var item in args)
        {
            Debug.Log("SampleMsg2: " + item.ToString());
        }
    }
}