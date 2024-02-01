using System;
namespace FastDev
{
    public interface IMsgManager<T> where T : class
    {
        void Dispatch(int msgID, T parameters);
        void Register(int msgID, Action<T> action);
        void UnRegister(int msgID, Action<T> action);
    }
}
