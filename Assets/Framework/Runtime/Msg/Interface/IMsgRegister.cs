using System;

namespace Framework
{
    public interface IMsgRegister
    {
        void Register(int msgID, Action<object[]> action);
        void UnRegister(int msgID, Action<object[]> action);
    }
}
