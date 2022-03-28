using UnityEngine;
namespace FastDev.UI
{
    /// <summary>
    /// 用于ILRunTime生成适配器
    /// </summary>
    public abstract class UIPanel : MonoBehaviour, IUIPanel
    {
        public abstract int index { get; }
        public abstract string panelName { get; }
        public abstract string assetPath { get; }

        public abstract void OnClose();
        public abstract void OnLoad(string assetPath);
        public abstract void OnOpen();
    }
}