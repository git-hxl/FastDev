using FastDev;
using System.Collections.Generic;
using UnityEngine;
namespace Framework
{
    public class UIManager : Singleton<UIManager>, IUIManager
    {
        public Dictionary<string, IUIPanel> UIPanels { get; private set; }

        protected override void OnInit()
        {
            base.OnInit();
            UIPanels = new Dictionary<string, IUIPanel>();
        }

        public IUIPanel GetUIPanel(string path)
        {
            IUIPanel uIPanel = null;
            if (UIPanels.ContainsKey(path))
                uIPanel = UIPanels[path];
            if (uIPanel == null)
            {
                GameObject uiAsset = AssetManager.Instance.LoadAsset<GameObject>("ui", path);
                GameObject uiObj = GameObject.Instantiate(uiAsset);
                uiObj.SetActive(false);
                uIPanel = uiObj.GetComponent<IUIPanel>();
                UIPanels[path] = uIPanel;
            }
            return uIPanel;
        }


        public T GetUIPanel<T>() where T : IUIPanel
        {
            foreach (var item in UIPanels)
            {
                if (item.Value is T)
                {
                    return (T)item.Value;
                }
            }
            return default(T);
        }


        public T GetUI<T>() where T : UIPanel
        {
            foreach (var item in UIPanels)
            {
                if (item.Value is T)
                {
                    return item.Value as T;
                }
            }
            return null;
        }
    }
}