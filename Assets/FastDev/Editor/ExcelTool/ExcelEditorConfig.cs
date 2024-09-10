
using System.IO;
using UnityEngine;

namespace FastDev.Editor
{
    public class ExcelEditorConfig
    {
        public static string ConfigPath { get; } = "./ExcelEditorConfig.json";

        public int StartHead { get; set; } = 4;
        public string InputExcelDir { get; set; }
        public string OutputJsonDir { get; set; }
        public string OutputCSDir { get; set; }

        public ExcelEditorConfig()
        {
            InputExcelDir = Application.streamingAssetsPath;
            OutputJsonDir = Application.streamingAssetsPath;

            OutputCSDir = Application.dataPath;
        }
    }
}
