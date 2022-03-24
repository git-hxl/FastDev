using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FastDev;
using System;

public class TcpServerExample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MiniTcpServer.Instance.Launch(8888);
    }
    private void OnDestroy()
    {
        MiniTcpServer.Instance.Close();
    }
}
