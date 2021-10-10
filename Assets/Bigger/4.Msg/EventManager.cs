using Bigger;
using System;
using System.Collections;
using System.Collections.Generic;
public class EventManager : MonoSingleton<EventManager>
{
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