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
        public override HashSet<KeyValuePair<string, object>> Preconditions { get; protected set; } = new HashSet<KeyValuePair<string, object>>();

        public override HashSet<KeyValuePair<string, object>> Effects { get; protected set; } = new HashSet<KeyValuePair<string, object>>();

        public override int Cost { get; protected set; } = 1;

        public PickupAxe(GoapAgent goapAgent):base(goapAgent)
        {
            Effects.Add(new KeyValuePair<string, object>(GlobalStateKey.HasAxe, true));
        }

        public override bool IsInRange()
        {
            return Vector3.Distance(Target.transform.position, GoapAgent.transform.position) < 0.01f;
        }

        public override bool CheckProceduralPrecondition()
        {
            Target = GameObject.FindGameObjectWithTag("Axe");

            return Target != null;
        }

        public override void OnStart()
        {
            //throw new NotImplementedException();
        }

        public override void OnRun()
        {
            IsDone = true;
            return;
        }
    }
}
