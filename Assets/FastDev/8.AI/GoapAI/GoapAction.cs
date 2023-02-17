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
        public bool IsDone { get; protected set; }

        public GoapAction(GoapAgent goapAgent)
        {
            GoapAgent = goapAgent;
        }

        public abstract bool IsInRange();
        public abstract bool CheckProceduralPrecondition();
        public virtual void OnStart()
        {
            IsDone = false;
        }
        public abstract void OnRun();
    }
}