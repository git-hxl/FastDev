using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FastDev
{
    public abstract class GoapAgent : MonoBehaviour, IGoapAgent
    {
        public List<IGoapAction> GoapActions { get; protected set; } = new List<IGoapAction>();

        public IGoapAction CurAction { get; set; }

        public IGoapAction Goal { get; set; }

        public GoapState GoapState { get; protected set; } = new GoapState();

        public GameObject Self => gameObject;

        private Queue<IGoapAction> queueGoapActions;

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
            Debug.LogError(goapAction.Name + ": Done");
            CurAction = null;
        }

        public virtual void OnActionPathFailed(IGoapAction goapAction)
        {
            Debug.LogError(goapAction.Name + ": OnActionPathFailed");
        }

        public virtual void OnPlanFailed()
        {
            Debug.LogError("OnPlanFailed");
        }

        private void Start()
        {
            if (Goal != null)
                queueGoapActions = GoapPlanner.Plan(this, Goal);
        }

        private void Update()
        {
            if (CurAction == null && queueGoapActions.Count > 0)
            {
                CurAction = queueGoapActions.Dequeue();
                CurAction.Start();
            }
            if (CurAction != null)
            {
                CurAction.Update();
            }
        }
    }
}
