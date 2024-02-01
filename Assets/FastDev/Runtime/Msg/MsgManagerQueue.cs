using Cysharp.Threading.Tasks;
using System;
using System.Collections.Concurrent;

namespace FastDev
{
    public class MsgManagerQuene<T> : Singleton<MsgManagerQuene<T>> where T : class
    {
        //采用线程安全的队列
        private ConcurrentQueue<MsgData<T>> msgQueue = new ConcurrentQueue<MsgData<T>>();

        protected override void OnInit()
        {
            base.OnInit();

            StartUpdate().Forget();
        }

        private async UniTaskVoid StartUpdate()
        {
            while (Instance != null)
            {
                await UniTask.Yield();

                while (!msgQueue.IsEmpty)
                {
                    MsgData<T> msgData;
                    if (msgQueue.TryDequeue(out msgData))
                    {
                       MsgManager<T>.Instance.Dispatch(msgData.msgID, msgData.parameters);
                    }
                }
            }
        }

        public void Enqueue(int msgID, T parameters)
        {
            MsgData<T> msgData = new MsgData<T>();
            msgData.msgID = msgID;
            msgData.parameters = parameters;
            msgQueue.Enqueue(msgData);
        }
    }
}