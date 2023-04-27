using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Framework
{
    public class MsgSyncManager : MonoSingleton<MsgSyncManager>
    {
        //采用线程安全的队列
        private ConcurrentQueue<MsgData> msgQueue = new ConcurrentQueue<MsgData>();

        private void Update()
        {
            while (!msgQueue.IsEmpty)
            {
                MsgData msgData;
                if (msgQueue.TryDequeue(out msgData))
                {
                    MsgManager.Instance.Dispatch(msgData.msgID, msgData.parameters);
                }
            }
        }

        public void Enqueue(int msgID, params object[] parameters)
        {
            MsgData msgData = new MsgData();
            msgData.msgID = msgID;
            msgData.parameters = parameters;
            msgQueue.Enqueue(msgData);
        }
    }
}