using System.Collections.Generic;
using UnityEngine;
namespace FastDev
{
    public abstract class GoapAgent : MonoBehaviour
    {
        public abstract HashSet<KeyValuePair<string, object>> WorldState { get; protected set; }

        public abstract HashSet<KeyValuePair<string, object>> GoalState { get; protected set; }

        public abstract HashSet<GoapAction> AllGoapActions { get; protected set; }

        public Stack<GoapAction> GoapActions { get; private set; }

        public GoapAction CurGoapAction { get; private set; }

        public abstract GoapPlanner GoapPlanner { get; protected set; }

        private void Awake()
        {
            OnInit();
        }

        public abstract void OnInit();
        public abstract void OnMove();

        public virtual void OnActionFailed()
        {
            Debug.LogError("OnActionFailed:" + CurGoapAction.ToString());
        }

        public virtual void OnActionDone()
        {
            Debug.Log("OnActionDone:" + CurGoapAction.ToString());
        }

        public virtual void OnPlanFailed(HashSet<KeyValuePair<string, object>> failedGoal)
        {
            Debug.LogError("OnPlanFailed:" + failedGoal.ToString());
        }

        public virtual void OnPlanDone(HashSet<KeyValuePair<string, object>> goal)
        {
            Debug.Log("OnPlanDone:" + goal.ToString());
        }

        public void StartPlan()
        {
            CurGoapAction = null;

            GoapActions = GoapPlanner.Plan(WorldState, GoalState);

            if (GoapActions == null)
            {
                OnPlanFailed(GoalState);
            }
        }

        private void Update()
        {
            if (GoapActions != null && GoapActions.Count > 0)
            {
                CurGoapAction = GoapActions.Peek();

                if (CurGoapAction.IsDone)
                {
                    OnActionDone();
                    GoapActions.Pop();


                    if (GoapActions.Count <= 0)
                    {
                        OnPlanDone(GoalState);
                    }

                    return;
                }

                if (CurGoapAction.Target == null)
                {
                    OnActionFailed();
                    return;
                }

                if (!CurGoapAction.IsInRange())
                {
                    OnMove();
                    return;
                }

                CurGoapAction.OnRun();
            }
        }
    }
}