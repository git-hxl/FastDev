using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

internal class CutTree : GoapAction
{
    private float time = 2f;
    private GameObject target;

    private GoapAgent goapAgent;

    public override bool CheckProcondition()
    {
        var target = GameObject.FindGameObjectWithTag("Tree");

        return target != null;
    }

    public override void OnInit(IGoapAgent goapAgent)
    {
        GoapActionState = GoapActionState.None;

        Preconditions.Clear();
        Preconditions.Add(new KeyValuePair<string, object>(GlobalStateKey.HasAxe, true));

        Effects.Clear();
        Effects.Add(new KeyValuePair<string, object>(GlobalStateKey.HasWood, true));

        Cost = 5;

        this.goapAgent = goapAgent as GoapAgent;
    }



    public override void OnStart()
    {
        target = GameObject.FindGameObjectWithTag("Tree");
        time = 2f;
    }

    public override void OnUpdate()
    {
        if (target == null)
        {
            GoapActionState = GoapActionState.End;
        }

        if (goapAgent == null)
        {
            GoapActionState = GoapActionState.End;
        }

        if (Vector3.Distance(target.transform.position, goapAgent.transform.position) > 0.01f)
        {

            goapAgent.transform.position = Vector3.MoveTowards(goapAgent.transform.position, target.transform.position, 5 * Time.deltaTime);
            return;
        }

        time -= Time.deltaTime;
        if (time < 0)
        {
            GoapActionState = GoapActionState.End;
        }
    }

    public override void OnEnd()
    {
        //throw new NotImplementedException();
    }
}