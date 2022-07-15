using FastDev;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class UITestPanel : UIPanel
{
    #region UIAttribute
	private Image imgRoot;
	public Image ImgRoot { get { if (imgRoot == null) { imgRoot = transform.Find("_Root").GetComponent<Image>(); } return imgRoot; } }
	private InputField inputAccount;
	public InputField InputAccount { get { if (inputAccount == null) { inputAccount = transform.Find("_Root/_Account").GetComponent<InputField>(); } return inputAccount; } }
	private Image imgPassword;
	public Image ImgPassword { get { if (imgPassword == null) { imgPassword = transform.Find("_Root/_Password").GetComponent<Image>(); } return imgPassword; } }
	private Button btConfirm;
	public Button BtConfirm { get { if (btConfirm == null) { btConfirm = transform.Find("_Root/_Confirm").GetComponent<Button>(); } return btConfirm; } }
	private RectTransform recttext;
	public RectTransform Recttext { get { if (recttext == null) { recttext = transform.Find("_Root/_text").GetComponent<RectTransform>(); } return recttext; } }
	#endregion UIAttribute

    public override void OnClose()
    {
        transform.GetChild(0).DOScale(0, 0.3f).SetEase(Ease.InCubic).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }

    public override void OnLoad()
    {
        BtConfirm.onClick.AddListener(Close);
    }

    public override void OnOpen()
    {
        gameObject.SetActive(true);
        transform.GetChild(0).localScale = Vector3.zero;
        transform.GetChild(0).DOScale(1, 0.3f).SetEase(Ease.OutCubic);
    }
}