using System.Collections.Generic;
using UnityEngine;
using FastDev.Res;
namespace FastDev.UI
{
    public class UIManager : MonoSingleton<UIManager>
    {
        private Dictionary<string, IUIPanel> _uiLoadedPanels = new Dictionary<string, IUIPanel>();
        private List<IUIPanel> _uiOpenedPanels = new List<IUIPanel>();
        /// <summary>
        /// 获取UI面板
        /// </summary>
        /// <param name="assetPath"></param>
        /// <returns></returns>
        public IUIPanel GetUI(string assetPath)
        {
            return LoadUIPanelFromAsset(assetPath);
        }
        /// <summary>
        /// 打开UI面板
        /// </summary>
        /// <param name="assetPath"></param>
        /// <returns></returns>
        public IUIPanel OpenUI(string assetPath)
        {
            var uiPanel = LoadUIPanelFromAsset(assetPath);
            return OpenUI(uiPanel);
        }
        /// <summary>
        /// 打开UI面板
        /// </summary>
        /// <param name="uiPanel"></param>
        /// <returns></returns>
        public IUIPanel OpenUI(IUIPanel uiPanel)
        {
            if (_uiOpenedPanels.Contains(uiPanel))
            {
                Debug.LogError("UIPanel has Opened:" + uiPanel.panelName);
            }
            else
            {
                uiPanel.OnOpen();
                _uiOpenedPanels.Add(uiPanel);
            }
            return uiPanel;
        }

        public IUIPanel Close(IUIPanel uiPanel)
        {
            if (_uiOpenedPanels.Contains(uiPanel))
            {
                uiPanel.OnClose();
                _uiOpenedPanels.Remove(uiPanel);
            }
            else
            {
                Debug.LogError("UIPanel is not opened:" + uiPanel.panelName);
            }
            return uiPanel;
        }
        /// <summary>
        /// 加载UI面板
        /// </summary>
        /// <param name="assetPath"></param>
        /// <returns></returns>
        private IUIPanel LoadUIPanelFromAsset(string assetPath)
        {
            if (!_uiLoadedPanels.ContainsKey(assetPath) || _uiLoadedPanels[assetPath].Equals(null))
            {
                GameObject assetObj = ResManager.instance.LoadAsset<GameObject>(ResConstant.ui, assetPath);
                GameObject panelObj = Instantiate(assetObj, transform);
                var panel = panelObj.GetComponent<IUIPanel>();
                if (panel != null)
                {
                    panel.OnLoad(assetPath);
                    _uiLoadedPanels[assetPath] = panel;
                    return panel;
                }
                Debug.LogError("UIPanel load Failed:" + assetPath);
                return null;
            }
            return _uiLoadedPanels[assetPath];
        }

        public override void Dispose()
        {
            base.Dispose();
            _uiLoadedPanels.Clear();
            _uiOpenedPanels.Clear();
            Resources.UnloadUnusedAssets();
        }
    }
}