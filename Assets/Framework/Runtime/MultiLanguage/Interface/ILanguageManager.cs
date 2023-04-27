using UnityEngine;

namespace Framework
{
    internal interface ILanguageManager:ILanguage,ILanguageRegister
    {
        void SetLanguageType(LanguageType languageType);
    }
}
