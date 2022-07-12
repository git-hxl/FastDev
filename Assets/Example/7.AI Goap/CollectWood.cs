using FastDev;
using UnityEngine;

public class CollectWood : GoapAction
{
    public override string Name { get; protected set; } = "收集木头";
    public override int Cost { get; protected set; } = 5;
    public override GoapState PreCondition { get; protected set; } = new GoapState(new System.Collections.Generic.Dictionary<string, object>() {
    });
    public override GoapState Effect { get; protected set; } = new GoapState(new System.Collections.Generic.Dictionary<string, object>() {
        {AIStateKey.Wood,true},
    });
    public override IGoapAgent Agent { get; protected set; }
    public override GameObject Target { get; protected set; } = GameObject.FindGameObjectWithTag("Wood");

    public CollectWood(IGoapAgent goapAgent) : base(goapAgent) { }

    protected override void OnDone()
    {

    }

    protected override void OnFailedByConditon()
    {

    }

    protected override void OnFailedByPath()
    {

    }

    private float time = 2;
    protected override void OnUpdate()
    {
        time -= Time.deltaTime;
        if (time <= 0)
            Agent.GoapState.SetValue(AIStateKey.Wood, true);
    }
}