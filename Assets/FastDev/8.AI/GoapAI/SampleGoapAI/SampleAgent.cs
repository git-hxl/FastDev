using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FastDev
{
    public class SampleAgent : GoapAgent
    {
        public override HashSet<KeyValuePair<string, object>> WorldState { get; protected set; }
        public override HashSet<KeyValuePair<string, object>> GoalState { get; protected set; }

        public override HashSet<GoapAction> AllGoapActions { get; protected set; }

        public override GoapPlanner GoapPlanner { get; protected set; }

        public override void OnInit()
        {
            WorldState = new HashSet<KeyValuePair<string, object>>();

            //WorldState.Add(new KeyValuePair<string, object>(GlobalStateKey.HasAxe, false));

            GoalState = new HashSet<KeyValuePair<string, object>>();
            GoalState.Add(new KeyValuePair<string, object>(GlobalStateKey.HasWood, true));

            AllGoapActions = new HashSet<GoapAction>();

            AllGoapActions.Add(new CutTree(this));
            AllGoapActions.Add(new PickupAxe(this));
            AllGoapActions.Add(new PickupWood(this));

            GoapPlanner = new GoapPlanner(this, AllGoapActions);
        }

        private void Start()
        {
            StartPlan();
        }


        public override void OnActionDone()
        {
            base.OnActionDone();
        }

        public override void OnActionFailed()
        {
            base.OnActionFailed();
        }

        public override void OnPlanFailed(HashSet<KeyValuePair<string, object>> failedGoal)
        {
            base.OnPlanFailed(failedGoal);
        }

        public override void OnPlanDone(HashSet<KeyValuePair<string, object>> goal)
        {
            base.OnPlanDone(goal);

            StartPlan();
        }

        public override void OnMove()
        {
            float step = 5 * Time.deltaTime;
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, CurGoapAction.Target.transform.position, step);
        }
    }
}