using System;
using System.Reflection;

namespace FastDev
{
    public struct MsgData<T> where T : class
    {
        public WeakReference target;//弱引用 防止内存泄漏
        public int msgID;
        public T parameters;
        public MethodInfo methodInfo;
    }

}
