
namespace Framework
{
    public interface IMsgDispatcher
    {
        void Dispatch(int msgID, params object[] parameters);
    }
}
