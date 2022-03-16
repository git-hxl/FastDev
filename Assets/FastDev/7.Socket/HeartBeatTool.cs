using System.Collections;
using UnityEngine;
namespace FastDev
{
    class HeartBeatTool : MonoBehaviour
    {
        private float checkInterval = 5;
        private float time = 0;
        private void Start()
        {
            MsgManager.instance.Register(MsgID.Ping, OnReceiveMsg);
            MsgManager.instance.Register(MsgID.ConnectSuccess, OnReceiveMsg);
        }

        private void OnReceiveMsg(Hashtable hashtable)
        {
            time = checkInterval; MiniTcpClient.Instance.Send(MsgID.Pong, null);
        }
        private void Update()
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
                if (time <= 0)
                {
                    Debug.LogError("心跳包接受失败！");
                    MsgManager.instance.Dispatch(MsgID.ConnectFailed, null);
                }
            }
        }
    }
}
