namespace GameFramework
{
    public interface IUIPanel
    {
        IUIPanel Open();
        void Close();
        void SetSorder(UIOrder uIOrder);
    }
}
