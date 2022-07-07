using System.Collections.Generic;

namespace FastDev
{
    public interface IAction
    {
        string Name { get; }
        Dictionary<string, object> PreConditions { get; }

        Dictionary<string, object> Effects { get; }

        int Cost { get; }

        bool IsInRange();

        bool IsReadyToRun();

        bool IsDone();

        void Update();
    }
}
