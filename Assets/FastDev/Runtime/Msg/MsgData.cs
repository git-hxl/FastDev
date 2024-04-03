using System;
using System.Reflection;

namespace FastDev
{
    public class MsgData<T> : IReference where T : class
    {
        public WeakReference target;//弱引用 防止内存泄漏
        public int msgID;
        public T parameters;
        public MethodInfo methodInfo;

        public void OnClear()
        {
            target = null;
            msgID = -1;
            parameters = null;
            methodInfo = null;
        }
    }

}
