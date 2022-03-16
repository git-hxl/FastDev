namespace FastDev.UI
{
    public interface IUIPanel
    {
        int index { get; }
        string panelName { get; }
        string assetPath { get; }

        void OnLoad(string assetPath);
        void OnOpen();
        void OnClose();
    }
}