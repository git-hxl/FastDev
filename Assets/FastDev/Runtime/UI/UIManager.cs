using System.Collections.Generic;
using UnityEngine;
namespace FastDev
{
    public class UIManager : MonoSingleton<UIManager>
    {
        public Dictionary<string, UIPanel> UIPanels { get; private set; }

        protected override void OnInit()
        {
            base.OnInit();

            UIPanels = new Dictionary<string, UIPanel>();
        }

        public T OpenUI<T>(string path, UIOrder uIOrder = UIOrder.Default) where T : UIPanel
        {
            UIPanel panel;

            if (!UIPanels.TryGetValue(path, out panel))
            {
                GameObject uiAsset = AssetManager.Instance.LoadAsset<GameObject>("ui", path);
                GameObject uiObj = GameObject.Instantiate(uiAsset, transform);

                panel = uiObj.GetComponent<T>();

                if (panel == null)
                {
                    Debug.LogError("UI Error:" + path);

                    return null;
                }

                UIPanels[path] = panel;

                panel.OnInit();
            }

            panel.Canvas.sortingOrder = (int)uIOrder;

            panel.OnOpen();

            return panel as T;
        }

        public void CloseUI(string path)
        {
            CloseUI(GetUI(path));
        }

        public void CloseUI(UIPanel uIPanel)
        {
            if (uIPanel != null)
            {
                uIPanel.Canvas.sortingOrder = (int)UIOrder.Hide;
                uIPanel.OnClose();
            }
        }

        public UIPanel GetUI<T>() where T : UIPanel
        {
            foreach (var item in UIPanels)
            {
                if (item.Value is T)
                {
                    return (T)item.Value;
                }
            }
            return null;
        }

        public UIPanel GetUI(string path)
        {
            if (UIPanels.ContainsKey(path))
            {
                return UIPanels[path];
            }
            return null;
        }
    }
}