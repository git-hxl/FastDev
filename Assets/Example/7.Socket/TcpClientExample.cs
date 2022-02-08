using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FastDev;
using System.Threading;
using System.Text;
using Cysharp.Threading.Tasks;
using System;
using System.Runtime.InteropServices;
using System.Data.Common;

public class TcpClientExample : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        EventManager.Instance.Register(EventMsgID.ConnectFailed, (data) => {
            MiniTcpClient.Instance.Close();
            ReConnect().Forget();
        });
        ReConnect().Forget();
    }

    private async UniTaskVoid ReConnect()
    {
        while (!await MiniTcpClient.Instance.Connect("127.0.0.1", 8888, 2000))
        {
            Debug.LogError("连接失败,3s后重连");
            await UniTask.Delay(3000);
        }
        Debug.Log("连接成功");
    }

    void MsgHandle(string data)
    {
        Debug.Log("消息延迟:" +data);
    }

    private void OnDestroy()
    {
        MiniTcpClient.Instance.Close();
    }

}
