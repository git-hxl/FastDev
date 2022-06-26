using FastDev;
using UnityEngine;
using UnityEngine.UI;

public class LanguageExample : MonoBehaviour
{
    public Text txt; 
    // Start is called before the first frame update
    void Start()
    {
        txt.text = LanguageManager.Instance.GetText(LanguageConstant.哈哈0哈_省略_1_字);
        MsgManager.Instance.Register(MsgID.OnLanguageChange, (a) =>
        {
            txt.text = LanguageManager.Instance.GetText(LanguageConstant.哈哈0哈_省略_1_字);
        });
    }

}
