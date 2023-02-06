
namespace FastDev
{
    public class LanguageData
    {
        public string Chinese="";
        public string English="";

        public LanguageData(string text)
        {
            Chinese = text;
        }

        public string GetText()
        {
            switch(LanguageManager.Instance.LanguageType)
            {
                case LanguageType.Chinese:return Chinese;
                case LanguageType.English: return English;
            }
            return "";
        }
    }
}
