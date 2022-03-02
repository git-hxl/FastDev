using System.Collections.Generic;
using UnityEngine;
namespace FastDev
{
    public class UIManager : MonoSingleton<UIManager>
    {
        public List<UIBase> openedPanels = new List<UIBase>();
        private Dictionary<string, UIBase> cachedPanels = new Dictionary<string, UIBase>();

        /// <summary>
        /// 获取或加载指定路径的UI
        /// </summary>
        /// <param name="path">该路径会自动处理从AB加载的情况</param>
        /// <returns></returns>
        public UIBase GetPanel(string path)
        {
            UIBase panel = null;
            if (cachedPanels.ContainsKey(path))
            {
                panel = cachedPanels[path];
                if (panel == null)
                {
                    cachedPanels.Remove(path);
                }
            }
            if (panel == null)
            {
                GameObject assetObj = ResManager.Instance.LoadAsset<GameObject>(ABConstant.ui, path);
                GameObject panelObj = Instantiate(assetObj, transform);
                panel = panelObj.GetComponent<UIBase>();
                if (panelObj != null)
                {
                    cachedPanels.Add(path, panel);
                }
                else
                {
                    Debug.LogError(assetObj.name + ":未找到Ipanel对象");
                }
            }
            return panel;
        }
        public override void Dispose()
        {
            base.Dispose();
            foreach (var item in cachedPanels)
            {
                Destroy(item.Value);
            }
            cachedPanels.Clear();
            openedPanels.Clear();
            Resources.UnloadUnusedAssets();
        }
    }
}