using System.Collections.Generic;
using UnityEngine;

namespace FastDev
{
    public interface IGoapAction
    {
        string Name { get; }

        int Cost { get; }

        GoapState PreCondition { get; }

        GoapState Effect { get; }

        IGoapAgent Agent { get; }

        GameObject Target { get; }

        bool CheckIsDone();

        bool CheckForRun();

        bool MoveToTarget();

        void Update();

        void OnDone();

        void OnFailed();
    }
}
