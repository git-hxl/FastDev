using System;
using System.Collections.Generic;
using static UnityEditor.Progress;

namespace FastDev
{
    public class MsgManager<T> : Singleton<MsgManager<T>>, IMsgManager<T> where T : class
    {
        private Dictionary<int, List<MsgData<T>>> actionDicts = new Dictionary<int, List<MsgData<T>>>();


        public void Register(int msgID, Action<T> action)
        {
            if (!actionDicts.ContainsKey(msgID))
            {
                List<MsgData<T>> msgDatas = new List<MsgData<T>>();
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
            MsgData<T> msgData = ReferencePool.Acquire<MsgData<T>>();
            msgData.target = new WeakReference(action.Target);
            msgData.msgID = msgID;
            msgData.methodInfo = action.Method;
            actionDicts[msgID].Add(msgData);
        }

        public void UnRegister(int msgID, Action<T> action)
        {
            if (actionDicts.ContainsKey(msgID))
            {
                foreach (var item in actionDicts[msgID])
                {
                    if (item.methodInfo == action.Method && item.target.Target == action.Target)
                    {
                        actionDicts[msgID].Remove(item);

                        ReferencePool.Release(item);
                        break;
                    }
                }
            }
        }

        public void Dispatch(int msgID, T parameters)
        {
            if (actionDicts.ContainsKey(msgID) && actionDicts[msgID] != null)
            {
                for (int i = actionDicts[msgID].Count - 1; i >= 0; i--)
                {
                    MsgData<T> msgData = actionDicts[msgID][i];

                    if (msgData.target.IsAlive && !msgData.target.Target.Equals(null))
                    {
                        msgData.methodInfo.Invoke(msgData.target.Target, new object[] { parameters });
                    }
                    else
                    {
                        actionDicts[msgID].RemoveAt(i);

                        ReferencePool.Release(msgData);
                    }
                }
            }
        }
    }
}