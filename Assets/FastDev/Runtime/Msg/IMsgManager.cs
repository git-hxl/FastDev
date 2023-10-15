using System;
namespace FastDev
{
    public interface IMsgManager
    {
        void Dispatch(int msgID, params object[] parameters);
        void Enqueue(int msgID, params object[] parameters);

        void Register(int msgID, Action<object[]> action);
        void UnRegister(int msgID, Action<object[]> action);
    }
}
