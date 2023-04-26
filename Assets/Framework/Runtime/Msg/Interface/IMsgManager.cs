using System;
namespace Framework
{
    public interface IMsgManager
    {
        void Register(int msgID, Action<object[]> action);
        void UnRegister(int msgID, Action<object[]> action);
        void Dispatch(int msgID, params object[] parameters);
    }
}
