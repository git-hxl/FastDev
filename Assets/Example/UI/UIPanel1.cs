using Bigger;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIPanel1 : UIPanel
{
    #region UIAttribute
	private Button _buttonbt;
	public Button buttonbt { get { if (_buttonbt == null) { _buttonbt = transform.Find("Image/_bt").GetComponent<Button>(); } return _buttonbt; } }
	private Button _buttonbt2;
	public Button buttonbt2 { get { if (_buttonbt2 == null) { _buttonbt2 = transform.Find("Image/_bt2").GetComponent<Button>(); } return _buttonbt2; } }
	private ScrollRect _scrollrectScrollView;
	public ScrollRect scrollrectScrollView { get { if (_scrollrectScrollView == null) { _scrollrectScrollView = transform.Find("Image/_Scroll View").GetComponent<ScrollRect>(); } return _scrollrectScrollView; } }
	private Image _imageHead;
	public Image imageHead { get { if (_imageHead == null) { _imageHead = transform.Find("Image/_Head").GetComponent<Image>(); } return _imageHead; } }
	private TextMeshProUGUI _textmeshprouguitext;
	public TextMeshProUGUI textmeshprouguitext { get { if (_textmeshprouguitext == null) { _textmeshprouguitext = transform.Find("Image/_text").GetComponent<TextMeshProUGUI>(); } return _textmeshprouguitext; } }
	#endregion UIAttribute
}