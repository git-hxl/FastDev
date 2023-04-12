using System;
using System.Collections.Generic;
using UnityEngine;

namespace FastDev
{
    internal class PickupAxe : GoapAction
    {
        public override HashSet<KeyValuePair<string, object>> Preconditions { get; protected set; } = new HashSet<KeyValuePair<string, object>>();

        public override HashSet<KeyValuePair<string, object>> Effects { get; protected set; } = new HashSet<KeyValuePair<string, object>>();

        public override int Cost { get; protected set; } = 1;

        public PickupAxe(GoapAgent goapAgent) : base(goapAgent)
        {
            Effects.Add(new KeyValuePair<string, object>(GlobalStateKey.HasAxe, true));
        }



        public override bool CheckProceduralPrecondition()
        {
            var target = GameObject.FindGameObjectWithTag("Axe");

            return target != null;
        }

        public override bool IsFailed()
        {
            return Target == null;
        }

        public override bool IsInRange()
        {
            return Vector3.Distance(Target.transform.position, GoapAgent.transform.position) < 0.01f;
        }

        public override void OnStart()
        {
            Target = GameObject.FindGameObjectWithTag("Axe");
        }

        public override void OnRun()
        {
            IsDone = true;
            return;
        }
    }
}
