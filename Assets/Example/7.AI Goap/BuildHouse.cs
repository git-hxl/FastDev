using FastDev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildHouse : IGoapAction
{
    public string Name => "建造房子";


    public int Cost => 5;

    public GoapState PreCondition => new GoapState(new Dictionary<string, int>() { { "wood", 10 }, { "ore", 10 } });

    public GoapState Effect => new GoapState(new Dictionary<string, int>() { { "wood", -10 }, { "ore", -10 } });

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
