using FastDev;

public static class UIExtension
{
    public static void OpenUI(this UIPanel panel)
    {
        UIManager.Instance.OpenUI(panel);
    }

    public static void CloseUI(this UIPanel panel)
    {
        UIManager.Instance.CloseUI(panel);
    }
}