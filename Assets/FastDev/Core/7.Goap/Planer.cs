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
            public List<IAction> Actions = new List<IAction>();
            public IAction Parent;
        }

        public static async void Plan(List<IAction> actions, IAction goal)
        {
            List<IAction> allReadyToRun = new List<IAction>();

            allReadyToRun.Add(goal);
            Debug.Log("add action:" + goal.Name);

            while (allReadyToRun[allReadyToRun.Count - 1].PreConditions.Count > 0)
            {
                await Task.Delay(1000);
                foreach (var item in allReadyToRun[allReadyToRun.Count - 1].PreConditions)
                {
                    List<IAction> availableActions = actions.Where((a) => a.Effects.ContainsKey(item.Key) && a.Effects[item.Key].Equals(item.Value)).ToList();
                    IAction action = GetMinCost(availableActions, item);

                    if (action == null)
                    {
                        Debug.LogError("当前目标不可达");
                        return;
                    }
                    allReadyToRun.Add(action);
                    await Task.Delay(1000);
                    Debug.Log("add action:" + action.Name);
                }
            }
        }


        private static IAction GetMinCost(List<IAction> actions, KeyValuePair<string, object> goal)
        {
            IAction min = null;
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
