using UnityEngine;
using UnityEngine.EventSystems;
namespace Bigger
{
    /// <summary>
    /// 用来控制UI窗口拖拽
    /// </summary>
    public class UIDrag : MonoBehaviour, IBeginDragHandler, IDragHandler
    {
        Vector3 offset;
        public void OnBeginDrag(PointerEventData eventData)
        {
            Vector3 pos;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(
                transform as RectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out pos);
            offset = transform.position - pos;
        }
        public void OnDrag(PointerEventData eventData)
        {
            Vector3 pos;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(
                transform as RectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out pos);
            transform.position = pos + offset;
        }
    }
}