using System.Collections.Generic;

namespace FastDev
{
    public interface IGoapAgent
    {
        HashSet<KeyValuePair<string, object>> WorldState { get; }

        HashSet<KeyValuePair<string, object>> GoalState { get; }

        HashSet<IGoapAction> AvailableActions { get; }

        GoapPlanner GoapPlanner { get; }

        Stack<IGoapAction> CurrentActions { get; }

        IGoapAction RunAction { get; }

        void Plan();

        void OnPlanFailed(HashSet<KeyValuePair<string, object>> failedGoal);

        void OnPlanDone(HashSet<KeyValuePair<string, object>> Goal);

        void OnActionFailed(IGoapAction failedAction);

        bool MoveToTarget(IGoapAction goapAction);
    }
}