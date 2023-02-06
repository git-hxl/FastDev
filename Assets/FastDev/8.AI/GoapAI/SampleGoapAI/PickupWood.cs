using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FastDev
{
    public class PickupWood : GoapAction
    {
        public override HashSet<KeyValuePair<string, object>> Preconditions { get; }

        public override HashSet<KeyValuePair<string, object>> Effects { get; }

        public override int Cost { get; }
        public override GameObject Target { get; protected set; }

        public PickupWood()
        {
            Preconditions = new HashSet<KeyValuePair<string, object>>();
            Effects = new HashSet<KeyValuePair<string, object>>();

            Cost = 10;

            Effects.Add(new KeyValuePair<string, object>(GlobalStateKey.HasWood, true));
        }

        public override bool CheckProceduralPrecondition(IGoapAgent goapAgent)
        {
            Target = GameObject.FindGameObjectWithTag("Wood");

            return Target != null;
        }

        public override bool RequireInRange()
        {
            return true;
        }

        public override bool Run(IGoapAgent goapAgent)
        {
            IsDone = true;
            return true;
        }
    }
}
