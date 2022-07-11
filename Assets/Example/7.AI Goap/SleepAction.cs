using FastDev;
using UnityEngine;

public class SleepAction : ProgressGoapAction
{
    public override string Name { get; protected set; } = "睡觉";
    public override int Cost { get; protected set; } = 5;
    public override float CostTime { get; protected set; } = -1f;
    public override float Progress { get; protected set; }
    public SleepAction(IGoapAgent agent) : base(agent) { }
    public override GoapState PreCondition { get; protected set; } = new GoapState(new System.Collections.Generic.Dictionary<string, int>()
    {
        {AIStateKey.HP,-999 },
    });

    public override GoapState Effect { get; protected set; } = new GoapState(new System.Collections.Generic.Dictionary<string, int>()
    {
         {AIStateKey.HP,100},
    });

    public override void SetTarget()
    {
        Target = GameObject.FindGameObjectWithTag("Bed");
    }

    public override bool CheckIsDone()
    {
        return Agent.GoapState.GetValue(AIStateKey.HP) >= 100;
    }

    public override void OnDone()
    {
        base.OnDone();
    }

    private float countTime;

    public override void Update()
    {
        if (CheckIsDone())
            return;
        if (!CheckForRun())
        {
            OnFailed();
            return;
        }
        if (MoveToTarget())
        {
            Agent.GoapState.AddValue(AIStateKey.HP, 1);
            Progress = Agent.GoapState.GetValue(AIStateKey.HP) / 100f;

            if (CheckIsDone())
                OnDone();
        }
    }
}