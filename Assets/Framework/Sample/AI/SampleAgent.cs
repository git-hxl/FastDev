using FastDev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleAgent : GoapAgent
{
    public override void OnInit()
    {
        base.OnInit();
        AllGoapActions.Add(new CutTree(this));
        AllGoapActions.Add(new PickupAxe(this));
        AllGoapActions.Add(new PickupWood(this));

    }

    private void Start()
    {
        StartPlan(new HashSet<KeyValuePair<string, object>> { new KeyValuePair<string, object>(GlobalStateKey.HasWood, true) });
    }

    public override void OnActionDone(GoapAction goapAction, bool isComplete)
    {
        base.OnActionDone(goapAction, isComplete);
        if (isComplete)
        {
            StartCoroutine(DelayReStart());
        }
    }

    public override void OnMove()
    {
        float step = 5 * Time.deltaTime;
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, CurGoapAction.Target.transform.position, step);
    }

    private IEnumerator DelayReStart()
    {
        yield return new WaitForSeconds(1);
        StartPlan(new HashSet<KeyValuePair<string, object>> { new KeyValuePair<string, object>(GlobalStateKey.HasWood, true) });
    }
}