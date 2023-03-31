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
        public override HashSet<KeyValuePair<string, object>> Preconditions { get; protected set; } = new HashSet<KeyValuePair<string, object>>();

        public override HashSet<KeyValuePair<string, object>> Effects { get; protected set; } = new HashSet<KeyValuePair<string, object>>();

        public override int Cost { get; protected set; } = 5;

        private float time = 2f;

        public CutTree(GoapAgent goapAgent) : base(goapAgent)
        {
            Preconditions.Add(new KeyValuePair<string, object>(GlobalStateKey.HasAxe, true));
            Effects.Add(new KeyValuePair<string, object>(GlobalStateKey.HasWood, true));
        }

        public override bool IsInRange()
        {
            return Vector3.Distance(Target.transform.position, GoapAgent.transform.position) < 0.01f;
        }


        public override bool CheckProceduralPrecondition()
        {
            Target = GameObject.FindGameObjectWithTag("Tree");

            return Target != null;
        }

        public override bool IsFailed()
        {
            return Target == null;
        }

        public override void OnStart()
        {
            base.OnStart();
            time = 2f;
        }


        public override void OnRun()
        {
            time -= Time.deltaTime;
            if (time < 0)
            {
                IsDone = true;
                return;
            }

            return;
        }


    }
}
