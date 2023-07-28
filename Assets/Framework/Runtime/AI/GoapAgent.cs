using System.Collections.Generic;
using UnityEngine;
namespace GameFramework
{
    public abstract class GoapAgent : MonoBehaviour, IGoapAgent
    {
        public HashSet<KeyValuePair<string, object>> WorldState { get; protected set; } = new HashSet<KeyValuePair<string, object>>();

        public HashSet<IGoapAction> GoapActions { get; protected set; } = new HashSet<IGoapAction> { };

        public Stack<IGoapAction> RunActions { get; protected set; }

        public IGoapAction CurGoapAction { get; protected set; }

        private void Awake()
        {
            OnInit();
        }

        public abstract void OnInit();

        public virtual void OnActionFailed(IGoapAction goapAction)
        {
            Debug.Log("OnActionFailed:<color=#FF0000>" + goapAction.ToString() + "</color>");
        }

        public virtual void OnActionDone(IGoapAction goapAction)
        {
            Debug.Log("OnActionDone:<color=#00FF00>" + goapAction.ToString() + "</color>");
        }

        public void OnUpdate()
        {
            if (RunActions != null && RunActions.Count > 0)
            {
                var goapAction = RunActions.Peek();

                if (CurGoapAction != goapAction)
                {
                    CurGoapAction = goapAction;
                    CurGoapAction.OnStart();
                }

                CurGoapAction.OnUpdate();

                if (CurGoapAction.GoapActionState == GoapActionState.End)
                {
                    CurGoapAction.OnEnd();
                    OnActionDone(CurGoapAction);
                    RunActions.Pop();
                    CurGoapAction = null;
                }
            }
        }

        private void Update()
        {
            OnUpdate();
        }
    }
}