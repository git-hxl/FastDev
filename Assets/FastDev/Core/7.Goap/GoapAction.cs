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

        public GoapActionState GoapActionState { get; protected set; }

        public float Progress { get; protected set; }

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

        public void Update()
        {
            //done
            if (CheckIsDone())
            {
                GoapActionState = GoapActionState.OnDone;
                OnDone();
                Agent.OnActionDone(this);
                GoapActionState = GoapActionState.None;
                return;
            }

            //check condition
            if (!CheckCondition())
            {
                OnFailedByConditon();
                Agent.OnActionConditionFailed(this);
                return;
            }

            //start
            if (GoapActionState == GoapActionState.None)
            {
                GoapActionState = GoapActionState.OnStart;
                Debug.Log(Name + " " + GoapActionState.ToString());
                OnStart();
            }

            //move
            if (!MoveToTarget())
            {
                GoapActionState = GoapActionState.OnMove;
                return;
            }

            //update
            GoapActionState = GoapActionState.OnUpdate;
            OnUpdate();
        }


        protected abstract void OnStart();
        protected abstract void OnDone();
        protected abstract void OnUpdate();
        protected abstract void OnFailedByConditon();
        protected abstract void OnFailedByPath();
    }
}
