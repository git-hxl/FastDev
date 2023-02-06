using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FastDev
{
    internal class PickupAxe : GoapAction
    {
        public override HashSet<KeyValuePair<string, object>> Preconditions { get; }

        public override HashSet<KeyValuePair<string, object>> Effects { get; }

        public override int Cost { get; }
        public override GameObject Target { get; protected set; }

        public PickupAxe()
        {
            Preconditions = new HashSet<KeyValuePair<string, object>>();
            Effects = new HashSet<KeyValuePair<string, object>>();

            Cost = 1;

            Effects.Add(new KeyValuePair<string, object>(GlobalStateKey.HasAxe, true));
        }

        public override bool CheckProceduralPrecondition(IGoapAgent goapAgent)
        {
            Target = GameObject.FindGameObjectWithTag("Axe");

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
