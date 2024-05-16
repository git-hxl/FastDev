
using System;
using UnityEngine;

namespace FastDev
{
    public class ObjectBase : IReference
    {
        private string objectName;
        private object target;

        protected void Init(string name, object target)
        {
            if (target == null)
            {
                Debug.LogError($"Target '{name}' is invalid.");
                return;
            }
            objectName = name;
            this.target = target;
        }

        public string Name { get { return objectName; } }

        public object Target { get { return target; } }

        public bool IsInUse { get; set; }

        /// <summary>
        /// 获取对象上次使用时间。
        /// </summary>
        public DateTime LastUseTime { get; set; }


        /// <summary>
        /// 获取对象时的事件。
        /// </summary>
        protected internal virtual void OnSpawn() { }
        /// <summary>
        /// 回收对象时的事件。
        /// </summary>
        protected internal virtual void OnUnspawn() { }

        /// <summary>
        /// 引用回收的事件
        /// </summary>
        public virtual void OnClear()
        {
            objectName = null;
            target = null;
        }

    }
}
