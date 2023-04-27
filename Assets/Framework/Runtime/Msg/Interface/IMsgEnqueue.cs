
namespace Framework
{
    public interface IMsgEnqueue
    {
        void Enqueue(int msgID, params object[] parameters);
    }
}
