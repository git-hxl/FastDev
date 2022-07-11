using FastDev;
using UnityEngine;

public class CollectStone : ProgressGoapAction
{
    public override string Name { get; protected set; } = "收集石头";
    public override int Cost { get; protected set; } = 5;
    public override float CostTime { get; protected set; } = 3f;
    public override float Progress { get; protected set; }
    public CollectStone(IGoapAgent agent):base(agent){ }
    public override GoapState PreCondition { get; protected set; } = new GoapState(new System.Collections.Generic.Dictionary<string, int>()
    {
        {AIStateKey.HP,100 },
    });

    public override GoapState Effect { get; protected set; } = new GoapState(new System.Collections.Generic.Dictionary<string, int>()
    {
         {AIStateKey.Stone,100},
    });

    public override void SetTarget()
    {
        Target = GameObject.FindGameObjectWithTag("Stone");
    }

    public override void OnDone()
    {
        base.OnDone();

        Agent.GoapState.AddValue(AIStateKey.HP, -50);
        Agent.GoapState.AddValue(AIStateKey.Stone, 100);
    }
}