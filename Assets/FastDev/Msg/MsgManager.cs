using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
namespace FastDev
{
    public class MsgManager : MonoSingleton<MsgManager>
    {
        struct MsgData
        {
            public int msgID;
            public Hashtable hashtable;
        }

        //采用线程安全的队列
        private ConcurrentQueue<MsgData> msgQueue = new ConcurrentQueue<MsgData>();

        private Dictionary<int, List<Action<Hashtable>>> actionDicts = new Dictionary<int, List<Action<Hashtable>>>();

        private void Update()
        {
            while (!msgQueue.IsEmpty)
            {
                MsgData msgData;
                if (msgQueue.TryDequeue(out msgData))
                {
                    Dispatch(msgData.msgID, msgData.hashtable);
                }
            }
        }

        public void Enqueue(int msgID, Hashtable hashtable)
        {
            MsgData msgData = new MsgData();
            msgData.msgID = msgID;
            msgData.hashtable = hashtable;
            msgQueue.Enqueue(msgData);
        }

        public void Register(int msgID, Action<Hashtable> action)
        {
            if (!actionDicts.ContainsKey(msgID))
                actionDicts.Add(msgID, new List<Action<Hashtable>>());

            if (!actionDicts[msgID].Contains(action))
                actionDicts[msgID].Add(action);
        }

        public void UnRegister(int msgID, Action<Hashtable> action)
        {
            if (actionDicts.ContainsKey(msgID))
            {
                if(actionDicts[msgID].Contains(action))
                {
                    actionDicts[msgID].Remove(action);
                }
            }
        }

        public void Dispatch(int msgID, Hashtable hashtable)
        {
            if (actionDicts.ContainsKey(msgID))
            {
                for (int i = actionDicts[msgID].Count - 1; i >= 0; i--)
                {
                    if (actionDicts[msgID][i].Target.Equals(null))
                        actionDicts[msgID].RemoveAt(i);
                    else
                        actionDicts[msgID][i]?.Invoke(hashtable);
                }
            }
        }
    }
}