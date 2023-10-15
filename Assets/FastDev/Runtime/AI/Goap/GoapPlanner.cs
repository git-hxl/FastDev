using FastDev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FastDev
{
    public class GoapPlanner
    {
        public static Stack<IGoapAction> Plan(IGoapAgent goapAgent, HashSet<KeyValuePair<string, object>> goal)
        {
            HashSet<IGoapAction> usableActions = new HashSet<IGoapAction>();
            foreach (IGoapAction a in goapAgent.GoapActions)
            {
                a.OnInit(goapAgent);
                if (a.CheckProcondition())
                {
                    usableActions.Add(a);
                }
            }

            List<GoapNode> findNodes = new List<GoapNode>();

            GoapNode start = new GoapNode(null, 0, goapAgent.WorldState, null);
            bool success = BuildGraph(start, findNodes, usableActions, goal);

            if (!success)
            {
                return null;
            }

            GoapNode cheapest = null;
            foreach (var leaf in findNodes)
            {
                if (cheapest == null)
                    cheapest = leaf;
                else
                {
                    if (leaf.Cost < cheapest.Cost)
                        cheapest = leaf;
                }
            }

            Stack<IGoapAction> stack = new Stack<IGoapAction>();

            List<string> debugs = new List<string>();
            GoapNode curNode = cheapest;
            while (curNode != null)
            {
                if (curNode.GoapAction != null)
                {
                    stack.Push(curNode.GoapAction);
                    debugs.Add("<color=#FFF000>" + curNode.GoapAction.ToString() + "</color>");
                }
                curNode = curNode.Parent;
            }
            debugs.Reverse();
            Debug.Log("GoapPlan:" + string.Join(">>", debugs));
            return stack;
        }


        private static bool BuildGraph(GoapNode parent, List<GoapNode> findNodes, HashSet<IGoapAction> goapActions, HashSet<KeyValuePair<string, object>> goal)
        {
            bool foundOne = false;
            foreach (var action in goapActions)
            {
                //满足前提执行条件
                if (Contains(parent.State, action.Preconditions))
                {
                    HashSet<KeyValuePair<string, object>> currentState = Combines(parent.State, action.Effects);

                    GoapNode goapNode = new GoapNode(parent, parent.Cost + action.Cost, currentState, action);

                    //新状态满足目标状态
                    if (Contains(currentState, goal))
                    {
                        findNodes.Add(goapNode);
                        foundOne = true;
                    }
                    else
                    {
                        HashSet<IGoapAction> subset = ActionSubset(goapActions, action);
                        bool found = BuildGraph(goapNode, findNodes, subset, goal);
                        if (found)
                            foundOne = true;
                    }
                }
            }
            return foundOne;
        }


        /// <summary>
        /// a 包含 b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static bool Contains(HashSet<KeyValuePair<string, object>> a, HashSet<KeyValuePair<string, object>> b)
        {
            foreach (var v in b)
            {
                bool result = false;

                foreach (var vv in a)
                {
                    if (vv.Equals(v))
                    {
                        result = true;
                        break;
                    }
                }

                if (result == false)
                {
                    return false;
                }
            }

            return true;
        }

        private static HashSet<KeyValuePair<string, object>> Combines(HashSet<KeyValuePair<string, object>> a, HashSet<KeyValuePair<string, object>> b)
        {
            HashSet<KeyValuePair<string, object>> state = new HashSet<KeyValuePair<string, object>>();

            foreach (KeyValuePair<string, object> v in a)
            {
                state.Add(new KeyValuePair<string, object>(v.Key, v.Value));
            }

            foreach (var v in b)
            {
                bool exists = false;
                foreach (var vv in state)
                {
                    if (vv.Key == v.Key)
                    {
                        exists = true;
                        break;
                    }
                }

                if (exists)
                {
                    state.RemoveWhere((KeyValuePair<string, object> kvp) => { return kvp.Key.Equals(v.Key); });
                    KeyValuePair<string, object> updated = new KeyValuePair<string, object>(v.Key, v.Value);
                    state.Add(updated);
                }
                else
                {
                    state.Add(new KeyValuePair<string, object>(v.Key, v.Value));
                }
            }

            return state;
        }

        private static HashSet<IGoapAction> ActionSubset(HashSet<IGoapAction> actions, IGoapAction removeAction)
        {
            HashSet<IGoapAction> subset = new HashSet<IGoapAction>();
            foreach (IGoapAction a in actions)
            {
                if (!a.Equals(removeAction))
                    subset.Add(a);
            }
            return subset;
        }
    }
}