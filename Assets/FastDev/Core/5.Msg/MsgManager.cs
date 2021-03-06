using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace FastDev
{
    public class MsgManager : MonoSingleton<MsgManager>
    {
        struct MsgData
        {
            public WeakReference target;//通过弱引用解决事件注册导致的内存泄漏
            public int msgID;
            public Hashtable param;
            public MethodInfo methodInfo;
        }

        //采用线程安全的队列
        private ConcurrentQueue<MsgData> msgQueue = new ConcurrentQueue<MsgData>();

        private Dictionary<int, List<MsgData>> actionDicts = new Dictionary<int, List<MsgData>>();

        private void Update()
        {
            while (!msgQueue.IsEmpty)
            {
                MsgData msgData;
                if (msgQueue.TryDequeue(out msgData))
                {
                    Dispatch(msgData.msgID, msgData.param);
                }
            }
        }

        public void Enqueue(int msgID, Hashtable param)
        {
            MsgData msgData = new MsgData();
            msgData.msgID = msgID;
            msgData.param = param;
            msgQueue.Enqueue(msgData);
        }

        public void Register(int msgID, Action<Hashtable> action)
        {
            if (!actionDicts.ContainsKey(msgID))
            {
                List<MsgData> msgDatas = new List<MsgData>();
                actionDicts.Add(msgID, msgDatas);
            }
            else
            {
                foreach (var item in actionDicts[msgID])
                {
                    if (item.methodInfo == action.Method && item.target.Target == action.Target)
                        return;
                }
            }
            MsgData msgData = new MsgData();
            msgData.target = new WeakReference(action.Target);
            msgData.msgID = msgID;
            msgData.methodInfo = action.Method;
            actionDicts[msgID].Add(msgData);
        }

        public void UnRegister(int msgID, Action<Hashtable> action)
        {
            if (actionDicts.ContainsKey(msgID))
            {
                foreach (var item in actionDicts[msgID])
                {
                    if (item.methodInfo == action.Method && item.target.Target == action.Target)
                    {
                        actionDicts[msgID].Remove(item);
                        break;
                    }
                }
            }
        }

        public void Dispatch(int msgID, Hashtable param)
        {
            if (actionDicts.ContainsKey(msgID) && actionDicts[msgID] != null)
            {
                for (int i = actionDicts[msgID].Count - 1; i >= 0; i--)
                {
                    if (actionDicts[msgID][i].target.IsAlive && !actionDicts[msgID][i].target.Target.Equals(null))
                    {
                        actionDicts[msgID][i].methodInfo.Invoke(actionDicts[msgID][i].target.Target, new object[] { param });
                    }
                    else
                    {
                        actionDicts[msgID].RemoveAt(i);
                    }
                }
            }
        }
    }
}