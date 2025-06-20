using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TouchInputDispatcher : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public UnityAction<Vector2> OnBeginTouch;
    public UnityAction<Vector2> OnMoveTouch;
    public UnityAction<Vector2> OnEndTouch;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        OnBeginTouch?.Invoke(eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnMoveTouch?.Invoke(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnEndTouch?.Invoke(eventData.position);
    }
}
