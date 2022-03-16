using FastDev.UI;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class UITestPanel : MonoBehaviour,IUIPanel
{
    #region UIAttribute
	private Button _buttonButton;
	public Button buttonButton { get { if (_buttonButton == null) { _buttonButton = transform.Find("Image/_Button").GetComponent<Button>(); } return _buttonButton; } }
    #endregion UIAttribute

    public int index => transform.GetSiblingIndex();

    public string panelName => gameObject.name;

    private string _assetPath;
    public string assetPath => _assetPath;

    public void OnClose()
    {
        transform.GetChild(0).DOScale(0, 0.3f).SetEase(Ease.InCubic).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }

    public void OnLoad(string assetPath)
    {
        _assetPath = assetPath;
        gameObject.SetActive(false);

        buttonButton.onClick.AddListener(() => {
            UIManager.instance.Close(this);
        });
    }

    public void OnOpen()
    {
        gameObject.SetActive(true);
        transform.GetChild(0).localScale = Vector3.zero;
        transform.GetChild(0).DOScale(1, 0.3f).SetEase(Ease.OutCubic);
    }

}