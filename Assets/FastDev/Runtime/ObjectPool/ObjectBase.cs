
using System;
using UnityEngine;

namespace FastDev
{
    public abstract class ObjectBase : MonoBehaviour, IReference
    {
        protected string assetName;
        protected GameObject target;

        public virtual void Init(string assetName, GameObject target)
        {
            if (target == null)
            {
                Debug.LogError($"Target '{assetName}' is invalid.");
                return;
            }
            this.assetName = assetName;
            this.target = target;
        }

        public string AssetName { get { return assetName; } }

        public bool IsInUse { get; set; }

        /// <summary>
        /// 获取对象上次使用时间。
        /// </summary>
        public DateTime LastUseTime { get; set; }

        /// <summary>
        /// 获取对象时的事件。
        /// </summary>
        public abstract void OnSpawn();
        /// <summary>
        /// 回收对象时的事件。
        /// </summary>
        public abstract void OnUnspawn();

        /// <summary>
        /// 引用回收的事件
        /// </summary>
        public virtual void OnClear()
        {
            target = null;
            assetName = null;
        }

    }
}
