namespace FastDev.UI
{
    public interface IUIPanel
    {
        void OnLoad(string assetPath);
        void OnOpen();
        void OnClose();
    }
}