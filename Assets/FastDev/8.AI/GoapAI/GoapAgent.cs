using System.Collections.Generic;
using UnityEngine;
namespace FastDev
{
    public abstract class GoapAgent : MonoBehaviour, IGoapAgent
    {
        public abstract HashSet<KeyValuePair<string, object>> WorldState { get; protected set; }

        public abstract HashSet<KeyValuePair<string, object>> GoalState { get; protected set; }

        public abstract HashSet<IGoapAction> AvailableActions { get; protected set; }

        public GoapPlanner GoapPlanner { get; private set; } = new GoapPlanner();

        public Stack<IGoapAction> CurrentActions { get; private set; }
        public IGoapAction RunAction { get; private set; }
        public abstract bool MoveToTarget(IGoapAction goapAction);

        public virtual void OnActionFailed(IGoapAction failedAction)
        {
            Debug.LogError("OnActionFailed:" + failedAction.ToString());
        }

        public virtual void OnPlanFailed(HashSet<KeyValuePair<string, object>> failedGoal)
        {
            Debug.LogError("OnPlanFailed:" + failedGoal.ToString());
        }

        public virtual void OnPlanDone(HashSet<KeyValuePair<string, object>> goal)
        {
            Debug.Log("OnPlanDone:" + goal.ToString());
        }

        public void Plan()
        {
            RunAction = null;
            CurrentActions = GoapPlanner.Plan(this, AvailableActions, WorldState, GoalState);

            if (CurrentActions == null)
            {
                OnPlanFailed(GoalState);
            }
        }

        private void Update()
        {
            if (CurrentActions != null && CurrentActions.Count > 0)
            {
                IGoapAction goapAction = CurrentActions.Peek();

                if (goapAction.IsDone)
                {
                    CurrentActions.Pop();
                    return;
                }

                if (goapAction != RunAction)
                {
                    RunAction = goapAction;
                    RunAction.Start();
                }

                if (goapAction.Target == null)
                {
                    OnActionFailed(goapAction);
                    return;
                }

                if (goapAction.RequireInRange())
                {
                    if (!MoveToTarget(goapAction))
                        return;
                }

                if (goapAction.Run(this))
                {
                    CurrentActions.Pop();
                }

                if (CurrentActions.Count <= 0)
                {
                    OnPlanDone(GoalState);
                }
            }
        }
    }
}