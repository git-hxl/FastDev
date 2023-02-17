
using System.Collections.Generic;

namespace FastDev
{
    public class GoapNode
    {
        public GoapNode Parent;
        public int Cost;
        public GoapAction GoapAction;

        public HashSet<KeyValuePair<string, object>> State;

        public GoapNode(GoapNode parent, int cost, HashSet<KeyValuePair<string, object>> state, GoapAction action)
        {
            this.Parent = parent;
            this.Cost = cost;
            this.State = state;
            this.GoapAction = action;
        }
    }

}
