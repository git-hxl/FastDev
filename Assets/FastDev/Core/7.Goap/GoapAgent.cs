using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FastDev
{
    public abstract class GoapAgent : MonoBehaviour, IGoapAgent
    {
        public List<IGoapAction> GoapActions { get; protected set; } = new List<IGoapAction>();

        public IGoapAction CurAction { get; set; }

        public IGoapAction Goal { get; private set; }

        public GoapState GoapState { get; protected set; } = new GoapState();

        public GameObject Self => gameObject;

        private Queue<IGoapAction> queueGoapActions;

        public void SetGoal(IGoapAction goal)
        {
            Goal = goal;

            if (Goal != null)
            {
                GoapState = new GoapState();
                queueGoapActions = GoapPlanner.Plan(this, Goal);
            }
                
        }

        public void AddAction(IGoapAction goapAction)
        {
            if (!GoapActions.Contains(goapAction))
            {
                GoapActions.Add(goapAction);
            }
        }

        public virtual void OnActionConditionFailed(IGoapAction goapAction)
        {
            Debug.LogError(goapAction.Name + ": OnActionConditionFailed");
            Debug.LogError("start rePlan...");
            if (Goal != null)
            {
                CurAction = null;
                queueGoapActions = GoapPlanner.Plan(this, Goal);
            }
        }

        public virtual void OnActionDone(IGoapAction goapAction)
        {
            CurAction = null;
            Debug.LogError(goapAction.Name + ": OnActionDone");
        }

        public virtual void OnActionPathFailed(IGoapAction goapAction)
        {
            Debug.LogError(goapAction.Name + ": OnActionPathFailed");
        }

        public virtual void OnPlanFailed()
        {
            Debug.LogError("OnPlanFailed");
        }

        protected virtual void Update()
        {
            if (CurAction == null && queueGoapActions.Count > 0)
            {
                CurAction = queueGoapActions.Dequeue();
            }
            if (CurAction != null)
            {
                CurAction.Update();
            }
        }
    }
}
