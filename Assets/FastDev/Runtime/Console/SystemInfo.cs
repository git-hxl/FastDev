using System.Text;
using UnityEngine;
namespace FastDev
{
    public class SystemInfo
    {
        public new static string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("内存：" + Utility.Converter.ByteConvert((long)UnityEngine.SystemInfo.systemMemorySize * 1024 * 1024));
            stringBuilder.AppendLine("显存：" + Utility.Converter.ByteConvert((long)UnityEngine.SystemInfo.graphicsMemorySize * 1024 * 1024));
            stringBuilder.AppendLine("分辨率：" + Screen.currentResolution.ToString());
            stringBuilder.AppendLine("设备名称：" + UnityEngine.SystemInfo.deviceName);
            stringBuilder.AppendLine("操作系统：" + UnityEngine.SystemInfo.operatingSystem);
            stringBuilder.AppendLine("处理器：" + UnityEngine.SystemInfo.processorType);
            stringBuilder.AppendLine("显卡：" + UnityEngine.SystemInfo.graphicsDeviceName);
            stringBuilder.AppendLine("DirectX版本：" + UnityEngine.SystemInfo.graphicsDeviceType.ToString());
            stringBuilder.AppendLine("主板型号：" + UnityEngine.SystemInfo.deviceModel);
            stringBuilder.AppendLine("设备ID：" + UnityEngine.SystemInfo.deviceUniqueIdentifier);

            return stringBuilder.ToString();
        }
    }
}
