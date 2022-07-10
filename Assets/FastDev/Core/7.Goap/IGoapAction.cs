using System.Collections.Generic;
using UnityEngine;

namespace FastDev
{
    public interface IGoapAction
    {
        string Name { get; }

        int Cost { get; }

        float Progress { get; }

        GoapState PreCondition { get; }

        GoapState Effect { get; }

        IGoapAgent Agent { get; }

        GameObject Target { get; }

        bool CheckForRun();

        bool MoveToTarget();

        bool CheckIsDone();

        void Update();

        void OnDone();

        void OnFailed();
    }
}
