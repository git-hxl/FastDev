using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleMsg : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MsgManager.Instance.Register(0, Test);
        MsgSyncManager.Instance.Register(0, Test);

        SampleMsg2 sampleMsg2 = new SampleMsg2();

        MsgManager.Instance.Register(0, sampleMsg2.Test_SampleMsg2);
    }

    private void Test(object[] arg)
    {
        Debug.Log(arg[0].ToString());
    }
    private void Test(byte[] arg)
    {
        Debug.Log(arg[0].ToString());
    }

}


public class SampleMsg2
{
    ~SampleMsg2()
    {
        Debug.LogError("SampleMsg2 析构执行");
    }

    public void Test_SampleMsg2(object[] arg)
    {
        Debug.Log(arg[0].ToString());
    }
}