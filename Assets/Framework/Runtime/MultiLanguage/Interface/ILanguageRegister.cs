
namespace Framework
{
    internal interface ILanguageRegister
    {
        LanguageData RegisterText(string text);
        bool RemoveLanguageData(LanguageData languageData);
    }
}
