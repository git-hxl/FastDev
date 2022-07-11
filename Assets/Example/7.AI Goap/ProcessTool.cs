using FastDev;
using UnityEngine;

public class ProcessTool : ProgressGoapAction
{
    public override string Name { get; protected set; } = "生产斧头";
    public override int Cost { get; protected set; } = 5;
    public override float CostTime { get; protected set; } = 3f;
    public override float Progress { get; protected set; }
    public ProcessTool(IGoapAgent agent) : base(agent) { }
    public override GoapState PreCondition { get; protected set; } = new GoapState(new System.Collections.Generic.Dictionary<string, int>()
    {
        {AIStateKey.HP,100 },
        {AIStateKey.WoodLog,100 },
         {AIStateKey.Stone,100 },
    });

    public override GoapState Effect { get; protected set; } = new GoapState(new System.Collections.Generic.Dictionary<string, int>()
    {
         {AIStateKey.Axe,10 },
    });

    public override void SetTarget()
    {
        Target = GameObject.FindGameObjectWithTag("Process");
    }

    public override void OnDone()
    {
        base.OnDone();
        Agent.GoapState.AddValue(AIStateKey.HP, -100);
        Agent.GoapState.AddValue(AIStateKey.Stone, -100);
        Agent.GoapState.AddValue(AIStateKey.WoodLog, -100);
        Agent.GoapState.AddValue(AIStateKey.Axe, 10);
    }
}