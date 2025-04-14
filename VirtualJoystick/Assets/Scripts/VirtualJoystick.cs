using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public Vector2 TouchPosition => _touchPosition;
    public float Horizontal => _touchAxis.x;
    public float Vertical => _touchAxis.y;

    [SerializeField] private RectTransform _rectTransform;
    [SerializeField, Range(0.2f, 0.5f)] private float _controllerBound;

    [Header("Image")]
    [SerializeField] private Image _background;
    [SerializeField] private Image _controller;

    private Vector2 _touchPosition;
    private Vector2 _touchAxis;

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, eventData.position, eventData.pressEventCamera, out _touchPosition))
        {
            _touchAxis.x = (_touchPosition.x / (_rectTransform.sizeDelta.x * _controllerBound));
            _touchAxis.y = (_touchPosition.y / (_rectTransform.sizeDelta.y * _controllerBound));
            _touchAxis = (_touchAxis.magnitude > 1.0f) ? _touchAxis.normalized : _touchAxis;

            _controller.rectTransform.anchoredPosition = new Vector2(
                _touchAxis.x * _rectTransform.sizeDelta.x * _controllerBound, 
                _touchAxis.y * _rectTransform.sizeDelta.y * _controllerBound
            );
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _controller.rectTransform.anchoredPosition = Vector2.zero;
        _touchPosition = Vector2.zero;
    }
}
