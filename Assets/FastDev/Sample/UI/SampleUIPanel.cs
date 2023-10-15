using FastDev;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SampleUIPanel : UIPanel
{
    #region
	private Image imgImage;
	public Image ImgImage { get { if (imgImage == null) { imgImage = transform.Find("_Image").GetComponent<Image>(); } return imgImage; } }
	private Button btButton;
	public Button BtButton { get { if (btButton == null) { btButton = transform.Find("_Button").GetComponent<Button>(); } return btButton; } }
    #endregion

    private void Start()
    {
        BtButton.onClick.AddListener(Close);
    }
}