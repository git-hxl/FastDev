
using UnityEngine;

namespace Framework.Editor
{
    public class JsonToolSetting
    {
        public static string SettingPath { get; } = "./jsontoolsetting.json";
        public string InputExcelDir;
        public string OutputJsonDir;

        public bool AutoParse;
        public JsonToolSetting()
        {
            InputExcelDir = Application.streamingAssetsPath + "/ExcelConfig";
            OutputJsonDir = Application.streamingAssetsPath + "/JsonConfig";
        }
    }
}
