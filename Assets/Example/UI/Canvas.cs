using Bigger;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Canvas : UIPanel
{
    #region UIAttribute
	private Image _imageImage;
	public Image imageImage { get { if (_imageImage == null) { _imageImage = transform.Find("_Image").GetComponent<Image>(); } return _imageImage; } }
	private Button _buttonButton;
	public Button buttonButton { get { if (_buttonButton == null) { _buttonButton = transform.Find("_Button").GetComponent<Button>(); } return _buttonButton; } }
	private Text _textText;
	public Text textText { get { if (_textText == null) { _textText = transform.Find("_Text").GetComponent<Text>(); } return _textText; } }
	#endregion UIAttribute
}