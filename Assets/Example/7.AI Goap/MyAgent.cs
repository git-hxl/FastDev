using FastDev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAgent : MonoBehaviour, IAgent
{
    private List<IAction> aiAcitons = new List<IAction>();
    public List<IAction> AIActions => aiAcitons;

    public void AddAction(IAction action)
    {
        if (!aiAcitons.Contains(action))
            aiAcitons.Add(action);
    }

    void Start()
    {
        AddAction(new BuildHouse());
        AddAction(new FellTreeAction());
        AddAction(new MachiningTree());
        AddAction(new MiningOre());
        Planer.Plan(aiAcitons, new BuildHouse());
    }
}
