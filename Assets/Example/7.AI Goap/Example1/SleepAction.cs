using Cysharp.Threading.Tasks;
using FastDev;
using UnityEngine;

public class SleepAction : GoapAction
{
    public override string Name { get; protected set; } = "睡觉";
    public override int Cost { get; protected set; } = 5;
    public override GoapState PreCondition { get; protected set; } = new GoapState(new System.Collections.Generic.Dictionary<string, int>()
    {

    });
    public override GoapState Effect { get; protected set; } = new GoapState(new System.Collections.Generic.Dictionary<string, int>() {
        {AIStateKey.HP,200},
    });
    public override IGoapAgent Agent { get; protected set; }
    public override GameObject Target { get; protected set; } = GameObject.FindGameObjectWithTag("Bed");

    public SleepAction(IGoapAgent goapAgent) : base(goapAgent) { }

    protected override void OnDone()
    {
        
    }

    protected override void OnFailedByConditon()
    {

    }

    protected override void OnFailedByPath()
    {

    }

    protected override void OnStart()
    {
        count = 0;
        Progress = 0;
    }

    private float time = 2;
    private float count = 0;
    protected override void OnUpdate()
    {
        Agent.GoapState.AddValue(AIStateKey.HP, 2);
    }
}