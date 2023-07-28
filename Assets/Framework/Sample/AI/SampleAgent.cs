using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleAgent : GoapAgent
{
    public override void OnInit()
    {
        GoapActions.Clear();
        GoapActions.Add(new CutTree());
        GoapActions.Add(new PickupAxe());
        GoapActions.Add(new PickupWood());
    }

    private void Start()
    {
        RunActions = GoapPlanner.Plan(this, new HashSet<KeyValuePair<string, object>> { new KeyValuePair<string, object>(GlobalStateKey.HasWood, true) });
    }

    public override void OnActionDone(IGoapAction goapAction)
    {
        base.OnActionDone(goapAction);
    }

    public void Move(Vector3 targetPos)
    {
        float step = 5 * Time.deltaTime;
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPos, step);
    }

}