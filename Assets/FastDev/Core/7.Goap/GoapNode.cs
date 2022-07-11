using System.Collections.Generic;

namespace FastDev
{
    public class GoapNode
    {
        public int Index = 0;
        public GoapNode Child;
        public GoapNode Parent;
        public GoapState GoapState = new GoapState();
        public List<IGoapAction> GoapActions = new List<IGoapAction>();

        public GoapNode(GoapNode parent)
        {
            this.Parent = parent;
            this.Index = parent != null ? parent.Index + 1 : 0;
        }
    }
}
