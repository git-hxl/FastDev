using System.Collections.Generic;
using UnityEngine;
namespace FastDev
{
    public class UIManager : MonoSingleton<UIManager>
    {
        private string bundleName = "ui";
        private Dictionary<string, UIPanel> panels = new Dictionary<string, UIPanel>();

        public Stack<UIPanel> OpenedPanels { get; private set; } = new Stack<UIPanel>();
        /// <summary>
        /// 加载UI面板
        /// </summary>
        /// <param name="assetPath"></param>
        /// <returns></returns>
        public UIPanel LoadPanel(string path)
        {
            UIPanel panel;
            panels.TryGetValue(path, out panel);
            if (panel == null || panel.Equals(null))
            {
                GameObject prefab = ResLoader.Instance.LoadAsset<GameObject>(bundleName, path);
                if (prefab != null)
                {
                    panel = Instantiate(prefab, transform).GetComponent<UIPanel>();
                    if (panel != null)
                    {
                        panel.OnLoad();
                        panels[path] = panel;
                    }
                }
            }
            return panel;
        }

        public void Open(UIPanel panel)
        {
            if (OpenedPanels.Contains(panel))
            {
                Debug.LogError("Open ui failed");
                return;
            }
            OpenedPanels.Push(panel);
            panel.OnOpen();
        }

        public void Close(UIPanel panel)
        {
            UIPanel peekPanel = OpenedPanels.Peek();
            if (peekPanel != panel)
            {
                Debug.LogError("close ui failed");
                return;
            }
            OpenedPanels.Pop();
            panel.OnClose();
        }


        public override void Dispose()
        {
            base.Dispose();
            panels.Clear();
            OpenedPanels.Clear();
            Resources.UnloadUnusedAssets();
        }
    }
}