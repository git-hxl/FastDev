using UnityEngine;

namespace GameFramework
{
    internal interface ILanguageManager
    {
        string GetText(string id);
        void SetLanguageType(LanguageType languageType);
    }
}
