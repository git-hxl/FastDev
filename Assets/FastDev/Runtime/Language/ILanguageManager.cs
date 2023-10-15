using UnityEngine;

namespace FastDev
{
    internal interface ILanguageManager
    {
        string GetText(string id);
        void SetLanguageType(LanguageType languageType);
    }
}
