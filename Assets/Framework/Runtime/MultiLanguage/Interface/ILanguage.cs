namespace Framework
{
    internal interface ILanguage
    {
        string GetText(int id);

        int GetID(string text);
    }
}
