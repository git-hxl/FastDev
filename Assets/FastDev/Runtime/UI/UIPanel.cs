using UnityEngine;
namespace FastDev.UI
{
    public abstract class UIPanel : MonoBehaviour, IUIPanel
    {
        public abstract void OnClose();
        public abstract void OnLoad(string assetPath);
        public abstract void OnOpen();
    }
}