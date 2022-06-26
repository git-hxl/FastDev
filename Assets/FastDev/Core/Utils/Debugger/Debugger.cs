using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace FastDev
{
    public class Debugger : MonoSingleton<Debugger>
    {
        private int frame;
        public int Frame { get { return frame; } }
        private int selectedToolBar;

        private Dictionary<WindowType, IWindow> windows = new Dictionary<WindowType, IWindow>();

        [HideInInspector]
        public float scale;
        private int heightScale = 1;

        private bool activeWindow;
        private void Start()
        {
            scale = 1f;
            CalculateFrame();
            RegisterWindow();
        }

        private void ActiveWindowListener()
        {
            if(Input.GetKeyDown(KeyCode.F1))
            {
                activeWindow = true;
                selectedToolBar = 0;
            }
        }

        private void RegisterWindow()
        {
            windows.Add(WindowType.Info, new SystemInfoWindow());
            windows.Add(WindowType.Log, new LogWindow());
            windows.Add(WindowType.Memory, new MemoryWindow());
            windows.Add(WindowType.Setting, new SettingWindow());
        }

        private void OnGUI()
        {
            ActiveWindowListener();
            if (!activeWindow)
                return;
            GUI.matrix = Matrix4x4.Scale(Vector3.one * scale);
            GUILayout.Window(0, new Rect(0, 0, Screen.width / 2, Screen.height / 2 * heightScale), DrawWindow, "Debugger");
        }

        private void DrawWindow(int id)
        {
            selectedToolBar = GUILayout.Toolbar(selectedToolBar, Enum.GetNames(typeof(WindowType)));
            var type = (WindowType)selectedToolBar;
            if (windows.ContainsKey(type))
            {
                windows[type].Draw();
                heightScale = 1;
            }
            else
            {
                activeWindow = false;
            }
        }

        private async void CalculateFrame()
        {
            while (true)
            {
                await UniTask.Delay(1000);
                frame = (int)(1 / Time.deltaTime);
            }
        }
    }
}
