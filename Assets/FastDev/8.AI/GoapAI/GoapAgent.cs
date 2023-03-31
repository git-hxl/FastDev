using System.Collections.Generic;
using UnityEngine;
namespace FastDev
{
    public abstract class GoapAgent : MonoBehaviour
    {
        public HashSet<KeyValuePair<string, object>> WorldState { get; set; }

        public HashSet<GoapAction> AllGoapActions { get; private set; }

        public Stack<GoapAction> PlanActions { get; private set; }

        public GoapPlanner GoapPlanner { get; private set; }

        public GoapAction GoapAction { get; private set; }

        private void Awake()
        {
            OnInit();
        }

        public virtual void OnInit()
        {
            WorldState = new HashSet<KeyValuePair<string, object>>();
            AllGoapActions = new HashSet<GoapAction>();
            GoapPlanner = new GoapPlanner(this);
        }

        public virtual void OnActionFailed(GoapAction goapAction)
        {
            Debug.LogError("OnActionFailed:" + goapAction.ToString());
        }

        public virtual void OnActionDone(GoapAction goapAction)
        {
            Debug.Log("OnActionDone:" + goapAction.ToString());
        }

        public virtual void OnPlanFailed()
        {
            Debug.LogError("OnPlanFailed");
        }

        public virtual void OnPlanDone()
        {
            Debug.Log("OnPlanDone");
        }

        public abstract void OnMove();

        public void StartPlan(HashSet<KeyValuePair<string, object>> goalState)
        {
            PlanActions = GoapPlanner.Plan(goalState);

            if (PlanActions == null)
            {
                OnPlanFailed();
            }
        }

        public void StopPlanActions()
        {
            PlanActions.Clear();
        }

        private void Update()
        {
            if (PlanActions != null && PlanActions.Count > 0)
            {
                GoapAction = PlanActions.Peek();

                if (GoapAction.IsDone)
                {
                    OnActionDone(GoapAction);
                    PlanActions.Pop();
                    if (PlanActions.Count <= 0)
                    {
                        OnPlanDone();
                    }
                    return;
                }

                if (GoapAction.IsFailed())
                {
                    OnActionFailed(GoapAction);
                    return;
                }

                if (!GoapAction.IsInRange())
                {
                    OnMove();
                    return;
                }

                if (!GoapAction.IsStart)
                {
                    GoapAction.OnStart();
                    return;
                }

                GoapAction.OnRun();
            }
        }
    }
}