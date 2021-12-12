using Bigger;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIPanel1 : UIPanel
{
    #region UIAttribute
	private Text _texttxtTitle;
	public Text texttxtTitle { get { if (_texttxtTitle == null) { _texttxtTitle = transform.Find("_txtTitle").GetComponent<Text>(); } return _texttxtTitle; } }
	private Button _buttonbtClose;
	public Button buttonbtClose { get { if (_buttonbtClose == null) { _buttonbtClose = transform.Find("_btClose").GetComponent<Button>(); } return _buttonbtClose; } }
	#endregion UIAttribute
    private void Start()
    {
        buttonbtClose.onClick.AddListener(Close);
    }
}