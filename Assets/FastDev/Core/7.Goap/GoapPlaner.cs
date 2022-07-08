using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace FastDev
{
    public static class Planer
    {
        public class Node
        {
            public Node Parent;
            public List<IGoapAction> GoapActions = new List<IGoapAction>();
        }

        public static async void Plan(List<IGoapAction> actions, IGoapAction goal)
        {
            Node node = new Node();
            node.GoapActions.Add(goal);

            foreach (var goalAction in node.GoapActions)
            {
                foreach (var preCondition in goalAction.PreCondition.Values)
                {
                    var selectedActions = actions.Where((a) => a.Effect.Values.ContainsKey(preCondition.Key) && a.Effect.Values[preCondition.Key] >= preCondition.Value).ToList();
                    var selectedAction = GetMinCost(selectedActions);
                    if (selectedAction == null)
                    {
                        Debug.LogError("目标不可达！！！");
                        return;
                    }
                    if (!node.GoapActions.Contains(selectedAction))
                    {
                        node.GoapActions.Add(selectedAction);
                        Debug.Log("Add Action:" + selectedAction.Name);
                    }
                }
            }
            
            
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

    }
}
