using System.Collections.Generic;
using UnityEngine;

namespace FastDev
{
    public class UIManager : MonoSingleton<UIManager>
    {
        public Dictionary<string, UIPanel> UIPanels { get; private set; } = new Dictionary<string, UIPanel>();

        public UIPanel LoadUIPanel(string path)
        {
            if (UIPanels.ContainsKey(path))
                return UIPanels[path];
            GameObject ui = AssetManager.Instance.LoadAsset<GameObject>("ui", path);
            UIPanel uIPanel = Instantiate(ui, transform).GetComponent<UIPanel>();
            UIPanels[path] = uIPanel;
            return uIPanel;
        }

        public UIPanel OpenUI(string path, UISortOrder sortOrder = UISortOrder.Middle)
        {
            UIPanel uIPanel = LoadUIPanel(path);
            uIPanel.Canvas.sortingOrder = (int)sortOrder;
            uIPanel.Open();
            uIPanel.transform.SetAsFirstSibling();
            return uIPanel;
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


        public void CloseUI(string path)
        {
            if (UIPanels.ContainsKey(path))
                UIPanels[path].Close();
        }

        public void HideAllActiveUI()
        {
            foreach (var panel in UIPanels)
            {
                panel.Value.Hide();
            }
        }

        public void ShowAllHidedUI()
        {
            foreach (var panel in UIPanels)
            {
                panel.Value.UnHide();
            }
        }
    }
}