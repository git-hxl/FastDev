using System.Collections.Generic;
using UnityEngine;
namespace FastDev
{
    public class UIManager : MonoSingleton<UIManager>
    {
        private List<UIPanel> curOpenedUIPanels = new List<UIPanel>();
        public Dictionary<string, UIPanel> UIPanels { get; private set; } = new Dictionary<string, UIPanel>();

        public UIPanel LoadUIPanel(string path)
        {
            if (UIPanels.ContainsKey(path))
                return UIPanels[path];
            GameObject ui = AssetManager.Instance.LoadAsset<GameObject>("ui", path);
            var uIPanel = Instantiate(ui, transform).GetComponent<UIPanel>();
            UIPanels[path] = uIPanel;
            return uIPanel;
        }

        public void OpenUI(UIPanel uIPanel)
        {
            if (!curOpenedUIPanels.Contains(uIPanel))
            {
                var topUi = GetTopActiveUI();
                if (topUi != null)
                    uIPanel.Canvas.sortingOrder = topUi.Canvas.sortingOrder + 1;

                curOpenedUIPanels.Add(uIPanel);
                uIPanel.OnOpen();
            }
        }

        public void CloseUI(UIPanel uIPanel)
        {
            if (curOpenedUIPanels.Contains(uIPanel))
            {
                uIPanel.Canvas.sortingOrder = 0;
                curOpenedUIPanels.Remove(uIPanel);
                uIPanel.OnClose();
            }
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

        public UIPanel GetTopActiveUI()
        {
            if (curOpenedUIPanels.Count > 0)
                return curOpenedUIPanels[curOpenedUIPanels.Count - 1];
            return null;
        }

        public void HideAllActiveUI()
        {
            foreach (var panel in UIPanels)
            {
                panel.Value.Canvas.enabled = false;
            }
        }

        public void ShowAllHidedUI()
        {
            foreach (var panel in UIPanels)
            {
                panel.Value.Canvas.enabled = true;
            }
        }
    }
}