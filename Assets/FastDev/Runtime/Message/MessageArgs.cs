
namespace FastDev
{
    public abstract class MessageArgs : IReference
    {
        public int MsgID {  get; set; }
        public abstract void OnClear();
    }
}
