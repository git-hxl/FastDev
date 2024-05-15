
using System;
using System.Collections.Generic;
namespace FastDev
{
    public sealed partial class MessageManager : GameModule
    {
        private Dictionary<int, List<Action<MessageArgs>>> actions;

        private readonly Queue<MessageArgs> queueArgs;

        public MessageManager()
        {
            actions = new Dictionary<int, List<Action<MessageArgs>>>();

            queueArgs = new Queue<MessageArgs>();
        }

        /// <summary>
        /// 注册消息
        /// </summary>
        /// <param name="msgID"></param>
        /// <param name="action"></param>
        public void Register(int msgID, Action<MessageArgs> action)
        {
            if (!actions.ContainsKey(msgID))
            {
                List<Action<MessageArgs>> msgDatas = new List<Action<MessageArgs>>();
                actions.Add(msgID, msgDatas);
            }
            actions[msgID].Add(action);
        }

        /// <summary>
        /// 取消注册消息
        /// </summary>
        /// <param name="msgID"></param>
        /// <param name="action"></param>
        public void UnRegister(int msgID, Action<MessageArgs> action)
        {
            if (actions.ContainsKey(msgID))
            {
                foreach (var item in actions[msgID])
                {
                    if (item == action)
                    {
                        actions[msgID].Remove(item);
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
        public void Dispatch(int msgID, MessageArgs args)
        {
            if (actions.ContainsKey(msgID))
            {
                for (int i = actions[msgID].Count - 1; i >= 0; i--)
                {
                    actions[msgID][i].Invoke(args);
                    ReferencePool.Release(args);
                }
            }
        }

        /// <summary>
        /// 分发消息 支持多线程
        /// </summary>
        /// <param name="msgID"></param>
        /// <param name="args"></param>
        public void EnqueueMsg(int msgID, MessageArgs args)
        {
            lock (queueArgs)
            {
                args.MsgID = msgID;
                queueArgs.Enqueue(args);
            }
        }


        /// <summary>
        /// 轮询消息队列
        /// </summary>
        /// <param name="elapseSeconds"></param>
        /// <param name="realElapseSeconds"></param>
        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
            lock (queueArgs)
            {
                while (queueArgs.Count > 0)
                {
                    MessageArgs args = queueArgs.Dequeue();
                    Dispatch(args.MsgID, args);
                }
            }
        }

        internal override void Shutdown()
        {
            //throw new NotImplementedException();
            actions.Clear();
            queueArgs.Clear();
        }
    }
}