
using System.IO;
using UnityEngine;

namespace FastDev.Editor
{
    public class ExcelToolConfig
    {
        public static string ConfigPath { get; } = "./ExcelToolConfig.json";

        public int ContentRow { get; set; } = 3;
        public string InputExcelDir { get; set; }
        public string OutputJsonDir { get; set; }

        public string OutputCSDir { get; set; }

        public bool AutoParse { get; set; }
        public ExcelToolConfig()
        {
            InputExcelDir = Application.streamingAssetsPath + "";
            OutputJsonDir = Application.streamingAssetsPath + "";

            OutputCSDir = Application.dataPath + "/FastDev.Game/GeneratedCode";
        }
    }
}
