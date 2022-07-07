using FastDev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachiningTree : IAction
{
    public string Name => "加工原木";

    public Dictionary<string, object> PreConditions => new Dictionary<string, object>() { { "hasTree", true } };

    public Dictionary<string, object> Effects => new Dictionary<string, object>() { { "hasTree", false }, { "hasWood", true } };

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
