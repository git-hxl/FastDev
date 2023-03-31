using System.Collections.Generic;
using UnityEngine;

namespace FastDev
{
    public abstract class GoapAction
    {
        public abstract HashSet<KeyValuePair<string, object>> Preconditions { get; protected set; }
        public abstract HashSet<KeyValuePair<string, object>> Effects { get; protected set; }
        public abstract int Cost { get; protected set; }
        public GoapAgent GoapAgent { get; protected set; }
        public GameObject Target { get; protected set; }
        public bool IsStart { get; protected set; }
        public bool IsDone { get; protected set; }

        public GoapAction(GoapAgent goapAgent)
        {
            GoapAgent = goapAgent;
        }

        public virtual bool CheckProceduralPrecondition()
        {
            return true;
        }

        public virtual void OnInit()
        {
            IsDone = false;
            IsStart = false;
            Target = null;
        }

        public virtual bool IsFailed()
        {
            return Target == null;
        }

        public abstract bool IsInRange();

        public virtual void OnStart()
        {
            IsStart = true;
        }

        public abstract void OnRun();
    }
}