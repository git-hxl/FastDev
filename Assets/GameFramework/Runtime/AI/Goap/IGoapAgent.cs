
using System.Collections.Generic;

namespace GameFramework
{
    public interface IGoapAgent
    {
        HashSet<KeyValuePair<string, object>> WorldState { get; }

        HashSet<IGoapAction> GoapActions { get; }

        Stack<IGoapAction> RunActions { get; }

        IGoapAction CurGoapAction { get; }

        void OnInit();

        void OnActionFailed(IGoapAction goapAction);

        void OnActionDone(IGoapAction goapAction);

        void OnUpdate();
    }
}
