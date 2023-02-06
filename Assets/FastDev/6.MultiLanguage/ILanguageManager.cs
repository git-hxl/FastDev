using UnityEngine;

namespace FastDev
{
    public interface ILanguageManager
    {
        string RegisterText(string text);

        void RemoveText(string text);

        string GetText(string id);

        void SetLanguageType(LanguageType languageType);
    }
}
