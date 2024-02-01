using System;
using System.Collections.Generic;

namespace FastDev
{
    public class MsgManager<T> : Singleton<MsgManager<T>>, IMsgManager<T>  where T : class
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
            MsgData<T> msgData = new MsgData<T>();
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
                    if (actionDicts[msgID][i].target.IsAlive && !actionDicts[msgID][i].target.Target.Equals(null))
                    {
                        actionDicts[msgID][i].methodInfo.Invoke(actionDicts[msgID][i].target.Target, new object[] { parameters });
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