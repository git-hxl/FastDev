
using System.IO;
using UnityEngine;

namespace FastDev.Editor
{
    public class ExcelToolSetting
    {
        public static string SettingPath { get; } = "./ExcelToJsonToolSetting.json";
        public int Head = 1;
        public string InputExcelDir;
        public string OutputJsonDir;

        public bool AutoParse;
        public ExcelToolSetting()
        {
            InputExcelDir = Application.streamingAssetsPath + "";
            OutputJsonDir = Application.streamingAssetsPath + "";
        }
    }
}
