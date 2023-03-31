using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FastDev
{
    public class SampleAgent : GoapAgent
    {
        public override void OnInit()
        {
            base .OnInit();
            AllGoapActions.Add(new CutTree(this));
            AllGoapActions.Add(new PickupAxe(this));
            AllGoapActions.Add(new PickupWood(this));

        }

        private void Start()
        {
            StartPlan(new HashSet<KeyValuePair<string, object>> { new KeyValuePair<string, object>(GlobalStateKey.HasWood, true) });
        }

        public override void OnPlanDone()
        {
            base.OnPlanDone();

            StartPlan(new HashSet<KeyValuePair<string, object>> { new KeyValuePair<string, object>(GlobalStateKey.HasWood, true) });
        }

        public override void OnMove()
        {
            float step = 5 * Time.deltaTime;
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, GoapAction.Target.transform.position, step);
        }
    }
}