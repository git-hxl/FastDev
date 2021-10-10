using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bigger
{
    public class CoroutineNode
    {
        private List<IEnumerator> nodes = new List<IEnumerator>();
        private Coroutine coroutine;

        private MonoBehaviour mono;

        public CoroutineNode(MonoBehaviour mono)
        {
            this.mono = mono;
        }

        public CoroutineNode AppendDelay(float delay)
        {
            nodes.Add(Delay(delay));
            return this;
        }
        private IEnumerator Delay(float delay)
        {
            yield return new WaitForSeconds(delay);
        }

        public CoroutineNode AppendEvent(Action action)
        {
            nodes.Add(Event(action));
            return this;
        }
        private IEnumerator Event(Action action)
        {
            action.Invoke();
            yield return null;
        }

        public CoroutineNode AppendUntil(Func<bool> condition)
        {
            nodes.Add(Until(condition));
            return this;
        }
        private IEnumerator Until(Func<bool> condition)
        {
            yield return new WaitUntil(condition);
        }

        public CoroutineNode AppendRepeat(int times, float interval, Action action, Func<bool> condition = null)
        {
            nodes.Add(Repeat(times, interval, action, condition));
            return this;
        }

        private IEnumerator Repeat(int times, float interval, Action action, Func<bool> condition = null)
        {
            while (times > 0 || times < 0)
            {
                if (condition != null && condition())
                {
                    yield break;
                }
                times--;
                yield return new WaitForSeconds(interval);
                action();
            }
        }
        private IEnumerator StartNode()
        {
            foreach (var cor in nodes)
            {
                yield return cor;
            }
        }

        public void Start()
        {
            coroutine = mono.StartCoroutine(StartNode());
        }
        public void Stop()
        {
            if (coroutine != null)
            {
                mono.StopCoroutine(coroutine);
                coroutine = null;
            }
        }
    }
}