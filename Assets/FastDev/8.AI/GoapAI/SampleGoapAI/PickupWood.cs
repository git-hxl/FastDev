using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FastDev
{
    public class PickupWood : GoapAction
    {
        public override HashSet<KeyValuePair<string, object>> Preconditions { get; protected set; } = new HashSet<KeyValuePair<string, object>>();

        public override HashSet<KeyValuePair<string, object>> Effects { get; protected set; } = new HashSet<KeyValuePair<string, object>>();

        public override int Cost { get; protected set; } = 10;
        public PickupWood(GoapAgent goapAgent) : base(goapAgent)
        {
            Cost = 10;

            Effects.Add(new KeyValuePair<string, object>(GlobalStateKey.HasWood, true));
        }

        public override bool IsInRange()
        {
            return Vector3.Distance(Target.transform.position, GoapAgent.transform.position) < 0.01f;
        }

        public override bool CheckProceduralPrecondition()
        {
            Target = GameObject.FindGameObjectWithTag("Wood");

            return Target != null;
        }

        public override void OnRun()
        {
            IsDone = true;
            return;
        }
    }
}
