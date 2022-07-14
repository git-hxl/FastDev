using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FastDev
{
    public static class GoapPlanner
    {
        public static Queue<IGoapAction> Plan(IGoapAgent goapAgent, IGoapAction goal)
        {
            Queue<IGoapAction> queueGoapActions = new Queue<IGoapAction>();

            GoapNode curNode = new GoapNode(null);
            curNode.GoapActions.Add(goal);

            while (true)
            {
                for (int i = 0; i < curNode.GoapActions.Count; i++)
                {
                    IGoapAction goalAction = curNode.GoapActions[i];
                    foreach (var preCondition in goalAction.PreCondition.Values)
                    {
                        //当前状态是否符合目标状态
                        if (ComPareState(goapAgent.GoapState, preCondition))
                            continue;
                        var selectedActions = GetMatchActions(goapAgent.GoapActions, preCondition);
                        var minCostAction = GetMinCost(selectedActions);
                        if (minCostAction == null)
                        {
                            Debug.LogError("目标不可达！！！:" + goalAction.Name);
                            goapAgent.OnPlanFailed();
                            return queueGoapActions;
                        }
                        if (curNode.Child == null)
                            curNode.Child = new GoapNode(curNode);
                        if (!curNode.Child.GoapActions.Contains(minCostAction))
                        {
                            curNode.Child.GoapActions.Add(minCostAction);
                        }
                    }
                }
                if (curNode.Child == null)
                    break;
                curNode = curNode.Child;
            }

            string actionPath = "";

            while (curNode != null)
            {
                foreach (var item in curNode.GoapActions)
                {
                    actionPath += item.Name + "->";
                    queueGoapActions.Enqueue(item);
                }
                curNode = curNode.Parent;
            }
            Debug.Log("Goap Action Plan：" + actionPath);

            return queueGoapActions;
        }

        private static List<IGoapAction> GetMatchActions(List<IGoapAction> actions, KeyValuePair<string, object> preCondition)
        {
            List<IGoapAction> matchActions = new List<IGoapAction>();
            foreach (var a in actions)
            {
                if (a.Effect.Values.ContainsKey(preCondition.Key) && a.Effect.Values[preCondition.Key].Equals(preCondition.Value))
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

        public static bool ComPareState(GoapState a, KeyValuePair<string, object> b)
        {
            if (!a.Values.ContainsKey(b.Key) || !a.Values[b.Key].Equals(b.Value))
                return false;
            return true;
        }

        public static bool ComPareState(GoapState a, GoapState b)
        {
            foreach (var state in b.Values)
            {
                if (!a.Values.ContainsKey(state.Key) || !a.Values[state.Key].Equals(state.Value))
                    return false;
            }
            return true;
        }
    }
}
