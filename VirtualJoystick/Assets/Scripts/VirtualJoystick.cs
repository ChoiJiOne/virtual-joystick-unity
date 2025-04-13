using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private Image _background;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Touch Begin : " + eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Touch & Drag : " + eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Touch Ended : " + eventData);
    }
}
