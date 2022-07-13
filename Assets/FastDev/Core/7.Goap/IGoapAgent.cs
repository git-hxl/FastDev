using System.Collections.Generic;
using UnityEngine;

namespace FastDev
{
    public interface IGoapAgent
    {
        List<IGoapAction> GoapActions { get; }
        IGoapAction CurAction { get; set; }
        IGoapAction Goal { get; }
        GoapState GoapState { get; }
        GameObject Self { get; }

        void SetGoal(IGoapAction goal);
        void AddAction(IGoapAction goapAction);
        void OnActionPathFailed(IGoapAction goapAction);
        void OnActionConditionFailed(IGoapAction goapAction);
        void OnActionDone(IGoapAction goapAction);
        void OnPlanFailed();
    }
}
