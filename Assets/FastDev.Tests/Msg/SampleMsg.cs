using FastDev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleMsg : MonoBehaviour
{
    SampleMsg2 sampleMsg2;
    // Start is called before the first frame update
    void Start()
    {
        MessageManager<string>.Instance.Register(MsgID.TestID, Test);

        sampleMsg2 = new SampleMsg2();

        MessageManager<int[]>.Instance.Register(MsgID.TestID, sampleMsg2.Test_SampleMsg2);
    }

    private void Test(string args)
    {
        Debug.Log("SampleMsg: " + args);
    }

    private void OnDestroy()
    {
        MessageManager<string>.Instance.UnRegister(MsgID.TestID, Test);

        MessageManager<int[]>.Instance.UnRegister(MsgID.TestID, sampleMsg2.Test_SampleMsg2);
    }

}


public class SampleMsg2
{
    ~SampleMsg2()
    {
        Debug.LogError("SampleMsg2 析构执行");
    }

    public void Test_SampleMsg2(int[] args)
    {
        foreach (var item in args)
        {
            Debug.Log("SampleMsg2: " + item + " "+Application.isPlaying);
        }
    }
}