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

    public static void SetSortToTop(this UIPanel panel)
    {
        var topUi = UIManager.Instance.GetTopActiveUI();
        if (topUi != null)
            panel.Canvas.sortingOrder = topUi.Canvas.sortingOrder + 1;
        else
            panel.Canvas.sortingOrder = 1;
    }
}