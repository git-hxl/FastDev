using FastDev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoOreAction : IGoapAction
{
    public string Name => "挖矿";

    public GoapState PreCondition => new GoapState(new Dictionary<string, int>());
    public GoapState Effect => new GoapState(new Dictionary<string, int>() { { "ore", 10 }});

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
