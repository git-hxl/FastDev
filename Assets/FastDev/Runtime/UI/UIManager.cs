using System.Collections.Generic;
using UnityEngine;
namespace FastDev
{
    public class UIManager : MonoSingleton<UIManager>
    {
        public Dictionary<string, UIPanel> UIPanels { get; private set; } = new Dictionary<string, UIPanel>();

        protected override void OnInit()
        {
            base.OnInit();
        }

        public UIPanel OpenUI(string path, UIOrder uIOrder = UIOrder.Default)
        {
            UIPanel uiPanel = null;
            if (UIPanels.ContainsKey(path))
                uiPanel = UIPanels[path];

            if (uiPanel == null)
            {
                GameObject uiAsset = AssetManager.Instance.LoadAsset<GameObject>("ui", path);
                GameObject uiObj = GameObject.Instantiate(uiAsset, transform);

                uiPanel = uiObj.GetComponent<UIPanel>();
                if (uiPanel == null)
                {
                    throw new System.Exception("打开窗口异常");
                }

                UIPanels[path] = uiPanel;

                uiPanel.OnInit();
            }

            uiPanel.Canvas.sortingOrder = (int)uIOrder;

            uiPanel.OnOpen();

            return uiPanel;
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