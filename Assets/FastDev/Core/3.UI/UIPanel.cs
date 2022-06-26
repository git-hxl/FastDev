using UnityEngine;
namespace FastDev
{
    public class UIPanel : MonoBehaviour, IPanel
    {
        public void Close()
        {
            gameObject.SetActive(false);
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }
    }
}