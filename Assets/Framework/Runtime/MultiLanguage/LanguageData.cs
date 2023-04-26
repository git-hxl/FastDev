namespace Framework
{
    public class LanguageData
    {
        public int ID;
        public string Chinese = "";
        public string English = "";

        public LanguageData(int id, string text)
        {
            this.ID = id;
            Chinese = text;
        }

        public string GetText()
        {
            switch (LanguageManager.Instance.LanguageType)
            {
                case LanguageType.Chinese: return Chinese;
                case LanguageType.English: return English;
            }
            return "";
        }
    }
}
