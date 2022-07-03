using UnityEngine;
using FastDev;
using System.Linq;
using System.Collections.Generic;

public class TestAgent : Agent
{
    void Start()
    {
        AddAction();
        SetGoal();
        CurState = new State() { Name = "Axe1", Value = true };
        List<AIAction> aIActions = this.Plan();
    }

    public override void AddAction()
    {
        actions = GetComponents<AIAction>().ToList();
    }

    public override void RemoveAction()
    {
        actions.Clear();
    }

    public override void SetGoal()
    {
        Goal = new State() { Name = "CollectWoodCount100", Value = true };
    }
}