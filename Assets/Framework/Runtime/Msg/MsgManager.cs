using System;
using System.Collections.Generic;
using System.Reflection;

namespace Framework
{
    public class MsgManager : Singleton<MsgManager>, IMsgManager
    {
        private Dictionary<int, List<MsgData>> actionDicts = new Dictionary<int, List<MsgData>>();

        public void Register(int msgID, Action<object[]> action)
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

        public void UnRegister(int msgID, Action<object[]> action)
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

        public void Dispatch(int msgID, params object[] parameters)
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