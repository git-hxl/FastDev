using System;
using System.Collections.Generic;

namespace FastDev
{
    public sealed partial class MessageManager : GameModule
    {
        private Dictionary<int, List<Delegate>> callBacks;

        public MessageManager()
        {
            callBacks = new Dictionary<int, List<Delegate>>();
        }

        /// <summary>
        /// 注册消息
        /// </summary>
        /// <param name="msgID"></param>
        /// <param name="action"></param>
        public void Register(int msgID, Action action)
        {
            if (!callBacks.ContainsKey(msgID))
            {
                List<Delegate> msgDatas = new List<Delegate>();
                callBacks.Add(msgID, msgDatas);
            }
            if (!callBacks[msgID].Contains(action))
                callBacks[msgID].Add(action);
        }

        public void Register<T1>(int msgID, Action<T1> action)
        {
            if (!callBacks.ContainsKey(msgID))
            {
                List<Delegate> msgDatas = new List<Delegate>();
                callBacks.Add(msgID, msgDatas);
            }
            if (!callBacks[msgID].Contains(action))
                callBacks[msgID].Add(action);
        }

        public void Register<T1, T2>(int msgID, Action<T1, T2> action)
        {
            if (!callBacks.ContainsKey(msgID))
            {
                List<Delegate> msgDatas = new List<Delegate>();
                callBacks.Add(msgID, msgDatas);
            }
            if (!callBacks[msgID].Contains(action))
                callBacks[msgID].Add(action);
        }

        public void Register<T1, T2, T3>(int msgID, Action<T1, T2, T3> action)
        {
            if (!callBacks.ContainsKey(msgID))
            {
                List<Delegate> msgDatas = new List<Delegate>();
                callBacks.Add(msgID, msgDatas);
            }
            if (!callBacks[msgID].Contains(action))
                callBacks[msgID].Add(action);
        }

        /// <summary>
        /// 取消注册消息
        /// </summary>
        /// <param name="msgID"></param>
        /// <param name="action"></param>
        public void UnRegister(int msgID, Action action)
        {
            if (callBacks.ContainsKey(msgID))
            {
                foreach (var item in callBacks[msgID])
                {
                    if (item.Equals(action))
                    {
                        callBacks[msgID].Remove(item);
                        break;
                    }
                }
            }
        }

        public void UnRegister<T1>(int msgID, Action<T1> action)
        {
            if (callBacks.ContainsKey(msgID))
            {
                foreach (var item in callBacks[msgID])
                {
                    if (item.Equals(action))
                    {
                        callBacks[msgID].Remove(item);
                        break;
                    }
                }
            }
        }
        public void UnRegister<T1, T2>(int msgID, Action<T1, T2> action)
        {
            if (callBacks.ContainsKey(msgID))
            {
                foreach (var item in callBacks[msgID])
                {
                    if (item.Equals(action))
                    {
                        callBacks[msgID].Remove(item);
                        break;
                    }
                }
            }
        }
        public void UnRegister<T1, T2, T3>(int msgID, Action<T1, T2, T3> action)
        {
            if (callBacks.ContainsKey(msgID))
            {
                foreach (var item in callBacks[msgID])
                {
                    if (item.Equals(action))
                    {
                        callBacks[msgID].Remove(item);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 分发消息
        /// </summary>
        /// <param name="msgID"></param>
        /// <param name="args"></param>
        public void Dispatch(int msgID)
        {
            if (callBacks.ContainsKey(msgID))
            {
                for (int i = 0; i < callBacks[msgID].Count; i++)
                {
                    if (callBacks[msgID][i] is Action)
                    {
                        ((Action)callBacks[msgID][i]).Invoke();
                    }
                }
            }
        }

        public void Dispatch<T1>(int msgID, T1 arg1)
        {
            if (callBacks.ContainsKey(msgID))
            {
                for (int i = 0; i < callBacks[msgID].Count; i++)
                {
                    if (callBacks[msgID][i] is Action<T1>)
                    {
                        ((Action<T1>)callBacks[msgID][i]).Invoke(arg1);
                    }
                }
            }
        }
        public void Dispatch<T1, T2>(int msgID, T1 arg1, T2 arg2)
        {
            if (callBacks.ContainsKey(msgID))
            {
                for (int i = 0; i < callBacks[msgID].Count; i++)
                {
                    if (callBacks[msgID][i] is Action<T1, T2>)
                    {
                        ((Action<T1, T2>)callBacks[msgID][i]).Invoke(arg1, arg2);
                    }
                }
            }
        }
        public void Dispatch<T1, T2, T3>(int msgID, T1 arg1, T2 arg2, T3 arg3)
        {
            if (callBacks.ContainsKey(msgID))
            {
                for (int i = 0; i < callBacks[msgID].Count; i++)
                {
                    if (callBacks[msgID][i] is Action<T1, T2, T3>)
                    {
                        ((Action<T1, T2, T3>)callBacks[msgID][i]).Invoke(arg1, arg2, arg3);
                    }
                }
            }
        }


        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {

        }

        internal override void Shutdown()
        {
            //throw new NotImplementedException();
            callBacks.Clear();
        }
    }
}