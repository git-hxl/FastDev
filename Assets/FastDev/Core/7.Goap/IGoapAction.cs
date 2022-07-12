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

        bool CheckCondition();

        bool CheckIsDone();

        bool MoveToTarget();

        void Start();

        void Update();
    }
}
