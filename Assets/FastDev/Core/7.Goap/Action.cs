using UnityEngine;
namespace FastDev
{
    /// <summary>
    /// AI 行为
    /// </summary>
    public abstract class AIAction : MonoBehaviour
    {
        //执行该Action的前提条件
        public State PreState;
        //完成Action的状态
        public State EndState;
        public int Cost;

        protected GameObject target;
        protected Agent agent;

        private void Awake()
        {
            agent = GetComponent<Agent>();
            SetPreState();
            SetEndState();
            SetTarget();
        }
        /// <summary>
        /// 判断什么时候结束该Action 
        /// </summary>
        /// <returns></returns>
        public abstract bool IsDone();
        /// <summary>
        /// 移动相关代码
        /// </summary>
        /// <returns></returns>
        protected abstract bool CheckIsInRange();
        /// <summary>
        /// 满足条件后执行的相关逻辑（每一帧执行一次）
        /// </summary>
        protected abstract void OnUpdate();
        protected abstract void SetPreState();
        protected abstract void SetEndState();
        protected abstract void SetTarget();

        public void AIUpdate()
        {
            if (IsDone())
            {
                agent.CurState = EndState;
                return;
            }
            if (agent.CurState.Name == PreState.Name && agent.CurState.Value)
            {
                if (!CheckIsInRange())
                    return;
                OnUpdate();
            }
        }
    }

}
