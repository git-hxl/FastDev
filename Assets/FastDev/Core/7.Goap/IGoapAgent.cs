using System.Collections.Generic;
using UnityEngine;

namespace FastDev
{
    public interface IGoapAgent
    {
        List<IGoapAction> AIActions { get; }
        IGoapAction CurAction { get; }
        GoapNode GoapNode { get; }
        GoapState GoapState { get; }

        GameObject Self { get; }
        void AddAction(IGoapAction action);
        void UpdateAction();

        void OnActionDone(IGoapAction goapAction);
        void OnActionFailed(IGoapAction goapAction);

        void OnPlanFailed();
    }
}
