using FastDev;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SampleUIPanel2 : UIPanel
{
    #region UIAttribute
    private Button btbtClose;
    public Button BtbtClose { get { if (btbtClose == null) { btbtClose = transform.Find("_btClose").GetComponent<Button>(); } return btbtClose; } }
    #endregion UIAttribute

    private void Start()
    {
        BtbtClose.onClick.AddListener(() =>
        {
            this.CloseUI();
        });
    }
}