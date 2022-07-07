using FastDev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FellTreeAction : IAction
{
    public string Name => "砍树";

    public Dictionary<string, object> PreConditions => new Dictionary<string, object>();

    public Dictionary<string, object> Effects => new Dictionary<string, object>() { { "hasTree", true } };

    public int Cost => 5;

    public bool IsDone()
    {
        throw new System.NotImplementedException();
    }

    public bool IsInRange()
    {
        throw new System.NotImplementedException();
    }

    public bool IsReadyToRun()
    {
        throw new System.NotImplementedException();
    }

    public void Update()
    {
        throw new System.NotImplementedException();
    }
}
