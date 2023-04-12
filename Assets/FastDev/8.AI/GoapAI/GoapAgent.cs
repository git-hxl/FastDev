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

        public GoapAction CurGoapAction { get; private set; }

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
            Debug.Log("OnActionFailed:<color=#FF0000>" + goapAction.ToString() + "</color>");
        }

        public virtual void OnActionDone(GoapAction goapAction, bool isComplete)
        {
            Debug.Log("OnActionDone:<color=#00FF00>" + goapAction.ToString() + "</color> result:" + isComplete);
        }

        public virtual void OnPlanDone(Stack<GoapAction> planActions)
        {
            if (planActions == null)
            {
                Debug.LogError("Plan Failed!!!");
                return;
            }
            this.PlanActions = planActions;
        }

        public abstract void OnMove();

        public void StartPlan(HashSet<KeyValuePair<string, object>> goalState)
        {
            this.CurGoapAction = null;
            var result = GoapPlanner.Plan(goalState);
            OnPlanDone(result);
        }

        public void StopPlanActions()
        {
            PlanActions.Clear();
        }

        private void Update()
        {
            if (PlanActions != null && PlanActions.Count > 0)
            {
                var goapAction = PlanActions.Peek();

                if (CurGoapAction != goapAction)
                {
                    CurGoapAction = goapAction;
                    CurGoapAction.OnStart();
                }

                if (CurGoapAction.IsFailed())
                {
                    OnActionFailed(CurGoapAction);
                    return;
                }

                if (!CurGoapAction.IsInRange())
                {
                    OnMove();
                    return;
                }

                CurGoapAction.OnRun();

                if (CurGoapAction.IsDone)
                {
                    PlanActions.Pop();
                    OnActionDone(CurGoapAction, PlanActions.Count <= 0);
                    CurGoapAction = null;
                }
            }
        }
    }
}