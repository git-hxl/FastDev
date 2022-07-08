using FastDev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAgent : MonoBehaviour, IGoapAgent
{
    private List<IGoapAction> aiAcitons = new List<IGoapAction>();
    public List<IGoapAction> AIActions => aiAcitons;

    public void AddAction(IGoapAction action)
    {
        if (!aiAcitons.Contains(action))
            aiAcitons.Add(action);
    }

    void Start()
    {
        AddAction(new BuildHouse());
        AddAction(new DoTreeAction());
        AddAction(new DoWoodAction());
        AddAction(new DoOreAction());
        Planer.Plan(aiAcitons, new BuildHouse());
    }
}
