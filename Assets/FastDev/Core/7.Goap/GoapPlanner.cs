using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FastDev
{
    public static class GoapPlanner
    {

        public static GoapNode Plan(IGoapAgent goapAgent, IGoapAction goal)
        {
            GoapNode curNode = new GoapNode(null);
            //curNode.GoapState = new GoapState(goal.PreCondition.Values);
            curNode.GoapActions.Add(goal);
            Debug.Log("Planner: add action " + goal.Name + " Node:" + curNode.Index);
            while (curNode.GoapActions.Count > 0)
            {
                for (int i = 0; i < curNode.GoapActions.Count; i++)
                {
                    var goalAction = curNode.GoapActions[i];
                    foreach (var preCondition in goalAction.PreCondition.Values)
                    {
                        if (ComPareState(goapAgent.GoapState, preCondition))
                            continue;
                        var selectedActions = GetMatchActions(goapAgent.AIActions, preCondition);
                        var minCostAction = GetMinCost(selectedActions);
                        if (minCostAction == null)
                        {
                            Debug.LogError("目标不可达！！！");
                            goapAgent.OnPlanFailed();
                            break;
                        }
                        if (curNode.Child == null)
                            curNode.Child = new GoapNode(curNode);
                        if (!curNode.Child.GoapActions.Contains(minCostAction))
                        {
                            curNode.Child.GoapActions.Add(minCostAction);
                            Debug.Log("Planner: add action " + minCostAction.Name + "->" + goalAction.Name + " Node:" + curNode.Child.Index);
                        }
                    }
                }
                if (curNode.Child == null)
                    break;
                curNode = curNode.Child;
            }
            return curNode;
        }

        private static List<IGoapAction> GetMatchActions(List<IGoapAction> actions, KeyValuePair<string, int> preCondition)
        {
            List<IGoapAction> matchActions = new List<IGoapAction>();
            foreach (var a in actions)
            {
                if (a.Effect.Values.ContainsKey(preCondition.Key) && a.Effect.Values[preCondition.Key] >= preCondition.Value)
                {
                    matchActions.Add(a);
                }
            }
            return matchActions;
        }

        private static IGoapAction GetMinCost(List<IGoapAction> actions)
        {
            IGoapAction min = null;
            foreach (var e in actions)
            {
                if (min == null || e.Cost < min.Cost)
                {
                    min = e;
                }
            }
            return min;
        }

        public static bool ComPareState(GoapState a, KeyValuePair<string, int> b)
        {
            if (!a.Values.ContainsKey(b.Key) || a.Values[b.Key] < b.Value)
                return false;
            return true;
        }

        public static bool ComPareState(GoapState a, GoapState b)
        {
            foreach (var state in b.Values)
            {
                if (!a.Values.ContainsKey(state.Key) || a.Values[state.Key] < state.Value)
                    return false;
            }
            return true;
        }

    }
}
