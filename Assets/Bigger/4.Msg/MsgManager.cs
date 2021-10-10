using Bigger;
using System.Collections;
using System.Collections.Concurrent;
public class MsgManager : MonoSingleton<MsgManager>
{
    //采用线程安全的队列
    private ConcurrentQueue<Hashtable> msgQueue = new ConcurrentQueue<Hashtable>();
    private void Update()
    {
        while (!msgQueue.IsEmpty)
        {
            Hashtable hashtable =  null;
            if (msgQueue.TryDequeue(out hashtable))
            {
                EventManager.Instance.Dispatch((int)hashtable["msgID"], hashtable);
            }
        }
    }

    public void Enqueue(int msgID, Hashtable hashtable)
    {
        hashtable.Add("msgID",msgID);
        msgQueue.Enqueue(hashtable);
    }
}