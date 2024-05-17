using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FastDev;

public class MessageA : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FastDev.GameEntry.Message.Register<int>(-1, OnCallback);
        FastDev.GameEntry.Message.Register<int>(-1, OnCallback);
        FastDev.GameEntry.Message.Register<int>(-1, OnCallback);
        FastDev.GameEntry.Message.Register<int>(-1, OnCallback);
        FastDev.GameEntry.Message.Register<int>(-1, OnCallback);
        FastDev.GameEntry.Message.Register<int>(-2, OnCallback);

        FastDev.GameEntry.Message.Register<int, string>(-1, OnCallback);
        FastDev.GameEntry.Message.Register<int, string>(-2, OnCallback);
    }

    void OnCallback(int value1)
    {
        Debug.Log(name + ":" + value1);
    }

    void OnCallback(int value1, string value2)
    {
        Debug.Log(name + ":" + value1 + " " + value2);
    }

    private void OnDestroy()
    {
        FastDev.GameEntry.Message.UnRegister<int>(-1, OnCallback);
        FastDev.GameEntry.Message.UnRegister<int>(-2, OnCallback);

        FastDev.GameEntry.Message.UnRegister<int, string>(-1, OnCallback);
        FastDev.GameEntry.Message.UnRegister<int, string>(-2, OnCallback);
    }
}
