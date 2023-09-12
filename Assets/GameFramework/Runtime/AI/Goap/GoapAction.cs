using GameFramework;
using System.Collections.Generic;
using UnityEngine;

namespace GameFramework
{
    public abstract class GoapAction : IGoapAction
    {
        public int Cost { get; protected set; }
        public HashSet<KeyValuePair<string, object>> Preconditions { get; protected set; } = new HashSet<KeyValuePair<string, object>>();
        public HashSet<KeyValuePair<string, object>> Effects { get; protected set; } = new HashSet<KeyValuePair<string, object>>();
        public GoapActionState GoapActionState { get; protected set; }
        public abstract void OnInit(IGoapAgent goapAgent);
        public abstract bool CheckProcondition();
        public abstract void OnStart();
        public abstract void OnUpdate();
        public abstract void OnEnd();

    }
}