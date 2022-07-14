using System.Collections.Generic;
using UnityEngine;
namespace FastDev
{
    public class UIManager : MonoSingleton<UIManager>
    {
        private string bundleName = "ui";
        private Dictionary<string, IPanel> panels = new Dictionary<string, IPanel>();

        public Stack<IPanel> OpenedPanels { get; set; } = new Stack<IPanel>();
        /// <summary>
        /// 加载UI面板
        /// </summary>
        /// <param name="assetPath"></param>
        /// <returns></returns>
        public IPanel LoadPanel(string path)
        {
            if (!panels.ContainsKey(path) || panels[path].Equals(null))
            {
                GameObject prefab = ResLoader.Instance.LoadAsset<GameObject>(bundleName, path);
                if (prefab != null)
                {
                    var panel = Instantiate(prefab, transform).GetComponent<IPanel>();

                    if (panel != null)
                    {
                        panel.Load();
                        panels[path] = panel;
                    }
                }
            }
            return panels[path];
        }

        public override void Dispose()
        {
            base.Dispose();
            panels.Clear();
            Resources.UnloadUnusedAssets();
        }
    }
}