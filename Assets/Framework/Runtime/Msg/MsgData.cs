using System;
using System.Reflection;

namespace Framework
{
    public struct MsgData
    {
        public WeakReference target;//通过弱引用解决事件注册导致的内存泄漏
        public int msgID;
        public object[] parameters;
        public MethodInfo methodInfo;
    }

}
