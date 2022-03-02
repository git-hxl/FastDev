using FastDev;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

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
                Dispatch((int)hashtable["msgID"], hashtable);
            }
        }
    }
    public void Enqueue(int msgID, Hashtable hashtable)
    {
        hashtable.Add("msgID",msgID);
        msgQueue.Enqueue(hashtable);
    }

    private Dictionary<int, List<Action<Hashtable>>> actionDicts = new Dictionary<int, List<Action<Hashtable>>>();

    public void Register(int eventID,Action<Hashtable> action)
    {
        if (!actionDicts.ContainsKey(eventID))
            actionDicts.Add(eventID, new List<Action<Hashtable>>());

        if (!actionDicts[eventID].Contains(action))
            actionDicts[eventID].Add(action);
    }

    public void Dispatch(int eventID,Hashtable hashtable)
    {
        if (actionDicts.ContainsKey(eventID))
        {
            for (int i = actionDicts[eventID].Count - 1; i >= 0; i--)
            {
                if (actionDicts[eventID][i].Target.Equals(null))
                    actionDicts[eventID].RemoveAt(i);
                else
                    actionDicts[eventID][i]?.Invoke(hashtable);
            }
        }
    }

}