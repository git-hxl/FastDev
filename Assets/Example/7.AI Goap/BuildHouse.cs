using FastDev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildHouse : IAction
{
    public string Name => "建造房子";

    public Dictionary<string, object> PreConditions => new Dictionary<string, object>() { { "hasWood", true }, { "hasOre", true } };

    public Dictionary<string, object> Effects => new Dictionary<string, object>() { { "hasWood", false }, { "hasOre", false } };

    public int Cost =>5;

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
