using UnityEngine;

namespace FastDev
{
    public abstract class ProgressGoapAction : IGoapAction
    {
        public abstract string Name { get; protected set; }

        public abstract int Cost { get; protected set; }

        public abstract float CostTime { get; protected set; }

        public abstract float Progress { get; protected set; }

        public abstract GoapState PreCondition { get; protected set; }

        public abstract GoapState Effect { get; protected set; }

        public IGoapAgent Agent { get; protected set; }

        public GameObject Target { get; protected set; }

        public ProgressGoapAction(IGoapAgent agent)
        {
            Agent = agent;
        }

        public virtual bool CheckIsDone()
        {
            if (CostTime > 0)
                return Progress >= 1;
            return false;
        }

        public virtual bool CheckForRun()
        {
            return GoapPlanner.ComPareState(Agent.GoapState, PreCondition);
        }

        public abstract void SetTarget();

        public virtual bool MoveToTarget()
        {
            if (Target == null)
                SetTarget();
            if (Target == null)
            {
                OnFailed();
                return false;
            }
            if (Vector3.Distance(Agent.Self.transform.position, Target.transform.position) > 1f)
            {
                Vector3 dir = (Target.transform.position - Agent.Self.transform.position).normalized;
                Agent.Self.transform.Translate(dir * 10 * Time.deltaTime);
                return false;
            }
            return true;
        }

        public virtual void OnDone()
        {
            Debug.Log(Name + ": Done!");
            Agent.OnActionDone(this);
        }

        public virtual void OnFailed()
        {
            Debug.LogError(Name + ": Run failed!");
            Agent.OnActionFailed(this);
        }

        private float countTime;
        public virtual void Update()
        {
            if (CheckIsDone())
                return;
            if (!CheckForRun())
            {
                OnFailed();
                return;
            }
            if (MoveToTarget())
            {
                countTime += Time.deltaTime;
                Progress = countTime / CostTime;
                if (CheckIsDone())
                    OnDone();
            }
        }
    }
}
