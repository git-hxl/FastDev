using UnityEngine;

namespace Framework
{
    public interface ILanguageManager
    {
        LanguageData RegisterText(string text);

        void RemoveText(int id);

        string GetText(int id);

        void SetLanguageType(LanguageType languageType);
    }
}
