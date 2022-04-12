using UnityEngine;

namespace FastDev
{
    class SettingWindow : IWindow
    {
        private Vector2 scrollPos;
        public void Draw()
        {
            scrollPos = GUILayout.BeginScrollView(scrollPos,"box");
            GUILayout.Label("窗口大小");
            Debugger.instance.scale = GUILayout.HorizontalSlider(Debugger.instance.scale, 0.5f, 2);
            GUILayout.Label("字体大小");
            GUI.skin.label.fontSize = (int)GUILayout.HorizontalSlider(GUI.skin.label.fontSize, 12, 50);

            GUILayout.EndScrollView();
        }
    }
}
