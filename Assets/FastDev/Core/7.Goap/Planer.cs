using System.Collections;
using System.Collections.Generic;
namespace FastDev
{
    public static class Planer
    {
        public static List<AIAction> Plan(this Agent agent)
        {
            List<AIAction> openList = new List<AIAction>();
            List<AIAction> closeList = new List<AIAction>();

            foreach (var item in agent.actions)
            {
                if (item.EndState.Name == agent.Goal.Name)
                {
                    openList.Add(item);
                    break;
                }
            }

            //倒叙A*搜索
            while (openList.Count > 0)
            {
                //从open列表中找到最小花费的行为
                AIAction min = GetMinCost(openList);
                openList.Remove(min);
                closeList.Add(min);
                //判断是否能够直接执行
                if (min.PreState == null || (agent.CurState != null && min.PreState.Name == agent.CurState.Name))
                    return closeList;
                //将能够满足该行为Conditiond的action添加到openlist
                foreach (var item in agent.actions)
                {
                    if (item.EndState.Name == min.PreState.Name)
                    {
                        openList.Add(item);
                    }
                }
            }
            return closeList;
        }


        private static AIAction GetMinCost(List<AIAction> list)
        {
            AIAction min = null;
            foreach (var e in list)
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
