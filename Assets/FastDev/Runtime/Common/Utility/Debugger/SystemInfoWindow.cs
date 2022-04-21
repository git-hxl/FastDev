using UnityEngine;

namespace FastDev
{
    class SystemInfoWindow : IWindow
    {
        private Vector2 scrollPos;
        public void Draw()
        {
            scrollPos = GUILayout.BeginScrollView(scrollPos, "box");
            GUILayout.Label("内存：" + UnitConvert.ByteConvert((long)SystemInfo.systemMemorySize * 1024 * 1024));
            GUILayout.Label("显存：" + UnitConvert.ByteConvert((long)SystemInfo.graphicsMemorySize * 1024 * 1024));
            GUILayout.Label("分辨率：" + Screen.currentResolution.ToString());
            GUILayout.Label("设备名称：" + SystemInfo.deviceName);
            GUILayout.Label("操作系统：" + SystemInfo.operatingSystem);
            GUILayout.Label("处理器：" + SystemInfo.processorType);
            GUILayout.Label("显卡：" + SystemInfo.graphicsDeviceName);
            GUILayout.Label("DirectX版本：" + SystemInfo.graphicsDeviceType.ToString());
            GUILayout.Label("主板型号：" + SystemInfo.deviceModel);
            GUILayout.Label("设备ID：" + SystemInfo.deviceUniqueIdentifier);
            GUILayout.EndScrollView();
        }

    }
}
