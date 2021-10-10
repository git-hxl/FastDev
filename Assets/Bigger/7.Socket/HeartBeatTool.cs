using System.Collections;
using UnityEngine;
namespace Bigger
{
    class HeartBeatTool : MonoBehaviour
    {
        private float checkInterval = 5;
        private float time = 0;
        private void Start()
        {
            EventManager.Instance.Register(MsgID.Ping, OnReceiveMsg);
            EventManager.Instance.Register(MsgID.ConnectSuccess, OnReceiveMsg);
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
                    EventManager.Instance.Dispatch(MsgID.ConnectFailed, null);
                }
            }
        }
    }
}
