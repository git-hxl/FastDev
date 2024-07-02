using FastDev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageB : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FastDev.MessageManager.Instance.Register<int>(-1, OnCallback);
        FastDev.MessageManager.Instance.Register<int>(-2, OnCallback);

        FastDev.MessageManager.Instance.Register<int, string>(-1, OnCallback);
        FastDev.MessageManager.Instance.Register<int, string>(-2, OnCallback);

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
        FastDev.MessageManager.Instance.UnRegister<int>(-1, OnCallback);
        FastDev.MessageManager.Instance.UnRegister<int>(-2, OnCallback);

        FastDev.MessageManager.Instance.UnRegister<int, string>(-1, OnCallback);
        FastDev.MessageManager.Instance.UnRegister<int, string>(-2, OnCallback);
    }
}
