using FastDev;
using System.Collections.Generic;
using UnityEngine;

public class PickupWood : GoapAction
{
    public override HashSet<KeyValuePair<string, object>> Preconditions { get; protected set; } = new HashSet<KeyValuePair<string, object>>();

    public override HashSet<KeyValuePair<string, object>> Effects { get; protected set; } = new HashSet<KeyValuePair<string, object>>();

    public override int Cost { get; protected set; } = 10;
    public PickupWood(GoapAgent goapAgent) : base(goapAgent)
    {
        Cost = 10;

        Effects.Add(new KeyValuePair<string, object>(GlobalStateKey.HasWood, true));
    }

    public override bool IsInRange()
    {
        return Vector3.Distance(Target.transform.position, GoapAgent.transform.position) < 0.01f;
    }

    public override bool CheckProceduralPrecondition()
    {
        var target = GameObject.FindGameObjectWithTag("Wood");

        return target != null;
    }

    public override void OnStart()
    {
        //throw new System.NotImplementedException();
        Target = GameObject.FindGameObjectWithTag("Wood");
    }

    public override void OnRun()
    {
        IsDone = true;
        return;
    }
}