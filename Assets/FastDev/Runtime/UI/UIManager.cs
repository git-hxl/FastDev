using System.Collections.Generic;
using UnityEngine;
using FastDev.Res;
namespace FastDev.UI
{
    public class UIManager : MonoSingleton<UIManager>
    {
        private Dictionary<string, UIPanel> uiLoadedPanels = new Dictionary<string, UIPanel>();
        private List<UIPanel> uiOpenedPanels = new List<UIPanel>();
        /// <summary>
        /// 获取UI面板
        /// </summary>
        /// <param name="assetPath"></param>
        /// <returns></returns>
        public UIPanel GetUI(string assetPath)
        {
            return LoadUIPanelFromAsset(assetPath);
        }
        /// <summary>
        /// 打开UI面板
        /// </summary>
        /// <param name="assetPath"></param>
        /// <returns></returns>
        public UIPanel OpenUI(string assetPath)
        {
            var uiPanel = LoadUIPanelFromAsset(assetPath);
            return OpenUI(uiPanel);
        }
        /// <summary>
        /// 打开UI面板
        /// </summary>
        /// <param name="uiPanel"></param>
        /// <returns></returns>
        public UIPanel OpenUI(UIPanel uiPanel)
        {
            if (uiPanel == null)
            {
                return null;
            }
            if (!uiOpenedPanels.Contains(uiPanel))
            {
                uiPanel.OnOpen();
                uiOpenedPanels.Add(uiPanel);
            }
            else
            {
                Debug.LogError("UI Open Failed: " + uiPanel.gameObject.name+". already Opened!");
            }
            return uiPanel;
        }

        public UIPanel Close(UIPanel uiPanel)
        {
            if (uiPanel == null)
            {
                if (uiOpenedPanels.Contains(uiPanel))
                    uiOpenedPanels.Remove(uiPanel);
                return null;
            }

            if (uiOpenedPanels.Contains(uiPanel))
            {
                uiPanel.OnClose();
                uiOpenedPanels.Remove(uiPanel);
            }
            else
            {
                Debug.LogError("UI Close Failed: " + uiPanel.gameObject.name+ ". already Closed!");
            }
            return uiPanel;
        }
        /// <summary>
        /// 加载UI面板
        /// </summary>
        /// <param name="assetPath"></param>
        /// <returns></returns>
        private UIPanel LoadUIPanelFromAsset(string assetPath)
        {
            if (!uiLoadedPanels.ContainsKey(assetPath) || uiLoadedPanels[assetPath].Equals(null))
            {
                GameObject assetObj = ResManager.Instance.LoadAsset<GameObject>(ABConstant.ui, assetPath);
                GameObject panelObj = Instantiate(assetObj, transform);
                var panel = panelObj.GetComponent<UIPanel>();
                if (panel != null)
                {
                    panel.OnLoad(assetPath);
                    uiLoadedPanels[assetPath] = panel;
                    return panel;
                }
                Debug.LogError("UIPanel load Failed:" + assetPath);
                return null;
            }
            return uiLoadedPanels[assetPath];
        }

        public override void Dispose()
        {
            base.Dispose();
            uiLoadedPanels.Clear();
            uiOpenedPanels.Clear();
            Resources.UnloadUnusedAssets();
        }
    }
}