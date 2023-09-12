
using System.Collections.Generic;

namespace GameFramework
{
    public interface IGoapAction
    {
        int Cost { get; }
        HashSet<KeyValuePair<string, object>> Preconditions { get; }
        HashSet<KeyValuePair<string, object>> Effects { get; }
        GoapActionState GoapActionState { get; }

        void OnInit(IGoapAgent goapAgent);
        bool CheckProcondition();
        void OnStart();
        void OnUpdate();
        void OnEnd();
    }
}
