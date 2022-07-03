using System.Collections.Generic;
using UnityEngine;
namespace FastDev
{
    public abstract class Agent:MonoBehaviour
    {
        public List<AIAction> actions = new List<AIAction>();
        
        public State CurState;
        public State Goal;
        public abstract void AddAction();
        public abstract void RemoveAction();
        public abstract void SetGoal();

        private void Update()
        {
            for (int i = actions.Count-1; i>=0; i--)
            {
                actions[i].AIUpdate();
            }
        }
    }
}
