
using FastDev;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TestUI : UIPanel
{
    #region
	private Image img_Bgg;
	public Image Img_Bgg { get { if (img_Bgg == null) { img_Bgg = transform.Find("_Bgg").GetComponent<Image>(); } return img_Bgg; } }
	private Image img_Bg;
	public Image Img_Bg { get { if (img_Bg == null) { img_Bg = transform.Find("Img_Bg").GetComponent<Image>(); } return img_Bg; } }
	private Button bt_Comfirm;
	public Button Bt_Comfirm { get { if (bt_Comfirm == null) { bt_Comfirm = transform.Find("Bt_Comfirm").GetComponent<Button>(); } return bt_Comfirm; } }
    #endregion


    private void Start()
    {
        Bt_Comfirm.onClick.AddListener(() =>
        {
            Img_Bgg.color = new Color(Random.value, Random.value, Random.value);

            Img_Bg.color = new Color(Random.value, Random.value, Random.value);
        });
    }

    private void OnEnable()
    {
        gameObject.SetActive(true);
    }
}