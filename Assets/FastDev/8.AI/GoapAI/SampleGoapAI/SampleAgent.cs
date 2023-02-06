using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FastDev
{
    public class SampleAgent : GoapAgent
    {
        public override HashSet<KeyValuePair<string, object>> WorldState { get; protected set; }
        public override HashSet<KeyValuePair<string, object>> GoalState { get; protected set; }

        public override HashSet<IGoapAction> AvailableActions { get; protected set; }
       
        private void Start()
        {
            WorldState = new HashSet<KeyValuePair<string, object>>();

            WorldState.Add(new KeyValuePair<string, object>(GlobalStateKey.HasAxe, false));

            GoalState = new HashSet<KeyValuePair<string, object>>();
            GoalState.Add(new KeyValuePair<string, object>(GlobalStateKey.HasWood, true));

            AvailableActions = new HashSet<IGoapAction>();

            AvailableActions.Add(new CutTree());
            AvailableActions.Add(new PickupAxe());
            AvailableActions.Add(new PickupWood());

            Plan();
        }



        public override void OnPlanDone(HashSet<KeyValuePair<string, object>> goal)
        {
            base.OnPlanDone(goal);


            Plan();
        }

        public override bool MoveToTarget(IGoapAction goapAction)
        {
            float step = 5 * Time.deltaTime;
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, goapAction.Target.transform.position, step);

            if (gameObject.transform.position.Equals(goapAction.Target.transform.position))
            {
                return true;
            }
            else
                return false;
        }
    }
}