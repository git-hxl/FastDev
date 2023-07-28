namespace GameFramework
{
    internal interface IUIManager
    {
        IUIPanel LoadUI(string path, UIOrder uIOrder = UIOrder.Default);

        bool HashUI(string path);

        bool HashUI<T>() where T : IUIPanel;

        T GetUI<T>() where T : IUIPanel;
    }
}
