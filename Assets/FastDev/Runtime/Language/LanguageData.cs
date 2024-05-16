namespace FastDev
{
    public class LanguageData
    {
        public string ID;//中文文本的HashCode
        public string Chinese = "";
        public string English = "";

        public LanguageData(string id, string text)
        {
            this.ID = id;
            Chinese = text;
        }

        public string GetText()
        {
            switch (GameEntry.Language.LanguageType)
            {
                case LanguageType.Chinese: return Chinese;
                case LanguageType.English: return English;
            }
            return "";
        }
    }
}
