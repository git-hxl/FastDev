using System;

public interface IMsgSyncManager
{
    void Enqueue(int msgID, byte[] data);
    void Register(int msgID, Action<byte[]> action);
    void UnRegister(int msgID, Action<byte[]> action);
    void Dispatch(int msgID, byte[] parameters);
}