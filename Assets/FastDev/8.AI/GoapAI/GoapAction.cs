using System.Collections.Generic;
using UnityEngine;

namespace FastDev
{
    public abstract class GoapAction : IGoapAction
    {
        public abstract HashSet<KeyValuePair<string, object>> Preconditions { get; }

        public abstract HashSet<KeyValuePair<string, object>> Effects { get; }

        public abstract int Cost { get; }

        public abstract GameObject Target { get; protected set; }

        public bool IsDone { get; protected set; }

        public virtual void Reset()
        {
            Target = null;
            IsDone = false;
        }
        public abstract bool RequireInRange();
        public virtual bool IsInRange(Vector3 pos)
        {
            if (Vector3.Distance(pos, Target.transform.position) < 0.1f)
            {
                return true;
            }
            return false;
        }
        public abstract bool CheckProceduralPrecondition(IGoapAgent goapAgent);
        public virtual void Start() { }
        public abstract bool Run(IGoapAgent goapAgent);
    }
}