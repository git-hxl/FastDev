using UnityEngine;

namespace FastDev
{
    class SettingWindow : IWindow
    {
        private float scale =1f;
        private Vector2 scrollPos;
        public void Draw()
        {
            scrollPos = GUILayout.BeginScrollView(scrollPos,"box");
            GUILayout.Label("缩放");
            GUI.matrix = Matrix4x4.Scale(Vector3.one * scale);
            scale = GUILayout.HorizontalSlider(scale, 0.2f, 1);
            GUILayout.EndScrollView();
        }
    }
}
