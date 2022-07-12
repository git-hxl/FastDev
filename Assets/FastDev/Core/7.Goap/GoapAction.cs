using UnityEngine;

namespace FastDev
{
    public abstract class GoapAction : IGoapAction
    {
        public abstract string Name { get; protected set; }

        public abstract int Cost { get; protected set; }

        public abstract GoapState PreCondition { get; protected set; }

        public abstract GoapState Effect { get; protected set; }

        public abstract IGoapAgent Agent { get; protected set; }

        public abstract GameObject Target { get; protected set; }

        public GoapAction(IGoapAgent goapAgent)
        {
            this.Agent = goapAgent;
        }

        public virtual bool CheckCondition()
        {
            return GoapPlanner.ComPareState(Agent.GoapState, PreCondition);
        }

        public virtual bool CheckIsDone()
        {
            return GoapPlanner.ComPareState(Agent.GoapState, Effect);
        }

        public virtual bool MoveToTarget()
        {
            //TODO:Path is not find
            //OnFailedByPath();
            //Agent.OnActionPathFailed(this);
            //return;
            if (Vector3.Distance(Agent.Self.transform.position, Target.transform.position) > 1f)
            {
                Vector3 dir = (Target.transform.position - Agent.Self.transform.position).normalized;
                Agent.Self.transform.Translate(dir * 10 * Time.deltaTime);
                return false;
            }
            return true;
        }

        public virtual void Start()
        {
            Debug.Log("Start:" + Name);
        }

        public void Update()
        {
            if (CheckIsDone())
            {
                OnDone();
                Agent.OnActionDone(this);
                return;
            }
            if (!CheckCondition())
            {
                OnFailedByConditon();
                Agent.OnActionConditionFailed(this);
                return;
            }
            if (!MoveToTarget())
                return;

            OnUpdate();
        }
        protected abstract void OnDone();
        protected abstract void OnUpdate();
        protected abstract void OnFailedByConditon();
        protected abstract void OnFailedByPath();
    }
}
