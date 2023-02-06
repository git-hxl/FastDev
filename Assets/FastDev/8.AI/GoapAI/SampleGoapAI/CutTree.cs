using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FastDev
{
    internal class CutTree : GoapAction
    {
        public override HashSet<KeyValuePair<string, object>> Preconditions { get; }

        public override HashSet<KeyValuePair<string, object>> Effects { get; }

        public override int Cost { get; }
        public override GameObject Target { get; protected set; }

        private float time = 2f;
        public CutTree()
        {
            Preconditions = new HashSet<KeyValuePair<string, object>>();
            Effects = new HashSet<KeyValuePair<string, object>>();

            Preconditions.Add(new KeyValuePair<string, object>(GlobalStateKey.HasAxe, true));

            Cost = 5;

            Effects.Add(new KeyValuePair<string, object>(GlobalStateKey.HasWood, true));


        }

        public override void Reset()
        {
            base.Reset();
            time = 2f;
        }

        public override bool CheckProceduralPrecondition(IGoapAgent goapAgent)
        {
            Target = GameObject.FindGameObjectWithTag("Tree");

            return Target != null;
        }

        public override bool RequireInRange()
        {
            return true;
        }

        public override bool Run(IGoapAgent goapAgent)
        {
            time -= Time.deltaTime;
            if (time < 0)
            {
                IsDone = true;
                return true;
            }

            return false;
        }
    }
}
