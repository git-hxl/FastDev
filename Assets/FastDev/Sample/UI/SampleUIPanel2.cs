using FastDev;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SampleUIPanel2 : UIPanel
{
    #region
	private Image imgImage;
	public Image ImgImage { get { if (imgImage == null) { imgImage = transform.Find("_Image").GetComponent<Image>(); } return imgImage; } }
	private Button btbtClose;
	public Button BtbtClose { get { if (btbtClose == null) { btbtClose = transform.Find("_btClose").GetComponent<Button>(); } return btbtClose; } }
	#endregion

    private void Start()
    {
        BtbtClose.onClick.AddListener(Close);
    }
}