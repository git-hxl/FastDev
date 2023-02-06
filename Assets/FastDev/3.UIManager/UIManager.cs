using System.Collections.Generic;
using UnityEngine;

namespace FastDev
{
    public class UIManager : MonoSingleton<UIManager>
    {
        public Dictionary<string, UIPanel> UIPanels { get; private set; } = new Dictionary<string, UIPanel>();

        private UIPanel LoadUIPanel(string path)
        {
            if (UIPanels.ContainsKey(path))
                return UIPanels[path];
            GameObject ui = AssetManager.Instance.LoadAsset<GameObject>("ui", path);
            UIPanel uIPanel = Instantiate(ui, transform).GetComponent<UIPanel>();
            UIPanels[path] = uIPanel;
            return uIPanel;
        }

        public void OpenUI(string path, UISortOrder sortOrder = UISortOrder.Middle)
        {
            UIPanel uIPanel = LoadUIPanel(path);
            uIPanel.Canvas.sortingOrder = (int)sortOrder;
            uIPanel.Open();
            uIPanel.transform.SetAsFirstSibling();
        }

        public void CloseUI(string path)
        {
            UIPanel uIPanel = LoadUIPanel(path);
            uIPanel.Close();
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