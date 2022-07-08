using System.Collections.Generic;

namespace FastDev
{
    public interface IGoapAction
    {
        string Name { get; }
        GoapState PreCondition { get; }

        GoapState Effect { get; }

        int Cost { get; }

        bool IsInRange();

        bool IsReadyToRun();

        bool IsDone();

        void Update();
    }
}
