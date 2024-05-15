using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace FastDev
{
    public sealed partial class UIManager : GameModule
    {
        private Dictionary<string, UIPanel> panels;


        public UIManager()
        {
            panels = new Dictionary<string, UIPanel>();
        }

        /// <summary>
        /// 加载UI
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private GameObject LoadPanel(string path)
        {
            GameObject uiAsset = GameEntry.Resource.LoadAsset<GameObject>("ui", path);
            GameObject uiObj = GameObject.Instantiate(uiAsset);
            return uiObj;
        }

        /// <summary>
        /// 获取UI
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public UIPanel GetUI(string path)
        {
            string key = Path.GetFileNameWithoutExtension(path);
            if (panels.ContainsKey(key))
            {
                return panels[key];
            }
            return null;
        }

        /// <summary>
        /// 打开UI
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="uIOrder"></param>
        /// <returns></returns>
        public T OpenUI<T>(string path, UIOrder uIOrder = UIOrder.Default) where T : UIPanel
        {
            UIPanel panel = null;
            string key = Path.GetFileNameWithoutExtension(path);

            if (!panels.ContainsKey(key))
            {
                GameObject uiObj = LoadPanel(path);
                panel = uiObj.GetComponent<T>();
                if (panel == null)
                {
                    Debug.LogError("UI Error:" + path);
                    return null;
                }
                panel.OnInit(key);
                panels.Add(key, panel);
            }

            panel = panels[key];
            panel.Canvas.sortingOrder = (int)uIOrder;
            panel.OnOpen();
            return panel as T;
        }

        /// <summary>
        /// 关闭UI
        /// </summary>
        /// <param name="path"></param>
        public void CloseUI(string path)
        {
            string key = Path.GetFileNameWithoutExtension(path);
            UIPanel uIPanel = GetUI(key);

            if (uIPanel != null)
            {
                uIPanel.Canvas.sortingOrder = (int)UIOrder.Hide;
                uIPanel.OnClose();
            }
        }

        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
            //throw new System.NotImplementedException();
        }

        internal override void Shutdown()
        {
            // throw new System.NotImplementedException();

            foreach (var item in panels)
            {
                GameEntry.Destroy(item.Value.gameObject);
            }

            panels.Clear();
        }
    }
}