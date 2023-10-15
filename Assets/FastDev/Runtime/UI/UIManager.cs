using System.Collections.Generic;
using UnityEngine;
namespace FastDev
{
    public class UIManager : MonoSingleton<UIManager>, IUIManager
    {
        public Dictionary<string, IUIPanel> UIPanels { get; private set; }

        protected override void OnInit()
        {
            base.OnInit();
            UIPanels = new Dictionary<string, IUIPanel>();
        }

        public IUIPanel LoadUI(string path, UIOrder uIOrder = UIOrder.Default)
        {
            IUIPanel uIPanel = null;
            if (UIPanels.ContainsKey(path))
                uIPanel = UIPanels[path];
            if (uIPanel == null)
            {
                GameObject uiAsset = AssetManager.Instance.LoadAsset<GameObject>("ui", path);
                GameObject uiObj = GameObject.Instantiate(uiAsset, transform);
                uiObj.SetActive(false);
                uIPanel = uiObj.GetComponent<IUIPanel>();
                uIPanel.SetSorder(uIOrder);
                UIPanels[path] = uIPanel;
            }
            return uIPanel;
        }

        public bool HashUI(string path)
        {
            return UIPanels.ContainsKey(path);
        }

        public bool HashUI<T>() where T : IUIPanel
        {
            foreach (var item in UIPanels)
            {
                if (item.Value is T)
                {
                    return true;
                }
            }
            return false;
        }


        public T GetUI<T>() where T : IUIPanel
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
    }
}