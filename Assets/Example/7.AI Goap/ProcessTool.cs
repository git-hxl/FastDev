using FastDev;
using UnityEngine;

public class ProcessTool : GoapAction
{
    public override string Name { get; protected set; } = "生产工具";
    public override int Cost { get; protected set; } = 5;
    public override GoapState PreCondition { get; protected set; } = new GoapState(new System.Collections.Generic.Dictionary<string, object>() {
          {AIStateKey.HP,true},
        {AIStateKey.WoodLog,true},
         {AIStateKey.Stone,true},
    });
    public override GoapState Effect { get; protected set; } = new GoapState(new System.Collections.Generic.Dictionary<string, object>() {
        {AIStateKey.Axe,true},
    });
    public override IGoapAgent Agent { get; protected set; }
    public override GameObject Target { get; protected set; } = GameObject.FindGameObjectWithTag("Process");

    public ProcessTool(IGoapAgent goapAgent) : base(goapAgent) { }

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
        {
            Agent.GoapState.SetValue(AIStateKey.Axe, true);

            Agent.GoapState.SetValue(AIStateKey.WoodLog, false);
            Agent.GoapState.SetValue(AIStateKey.Stone, false);
            Agent.GoapState.SetValue(AIStateKey.HP, false);
        }
          
    }
}