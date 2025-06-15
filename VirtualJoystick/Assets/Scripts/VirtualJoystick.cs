using UnityEngine;

public class VirtualJoystick : MonoBehaviour
{
    public bool IsDrag;
    public Vector2 Axis => _touchAxis;
    
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TouchInputDispatcher _touchInputDispatcher;
    [SerializeField] private RectTransform _rootRect;

    [Header("Background")]
    [SerializeField] private GameObject _background;
    [SerializeField] private RectTransform _backgroundRect;

    [Header("Handle")]
    [SerializeField] private GameObject _handle;
    [SerializeField] private RectTransform _handleRect;
    [SerializeField, Range(0.2f, 1.0f)] private float _handleBound;

    private RectTransform _canvasRect;
    private Vector2 _touchPosition;
    private Vector2 _touchAxis;

    private void Awake()
    {
        _touchInputDispatcher.OnBeginTouch += OnBeginTouch;
        _touchInputDispatcher.OnMoveTouch += OnMoveTouch;
        _touchInputDispatcher.OnEndTouch += OnEndTouch;

        _canvasRect = _canvas.transform as RectTransform;
    }

    private void OnDestroy()
    {
        _touchInputDispatcher.OnBeginTouch -= OnBeginTouch;
        _touchInputDispatcher.OnMoveTouch -= OnMoveTouch;
        _touchInputDispatcher.OnEndTouch -= OnEndTouch;
    }

    private void OnBeginTouch(Vector2 touchPosition)
    {
        _touchAxis = Vector2.zero;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvasRect, touchPosition, _canvas.worldCamera, out _touchPosition))
        {
            _rootRect.anchoredPosition = _touchPosition;
        }

        IsDrag = true;
        SetVisibleUI(IsDrag);
    }

    private void OnMoveTouch(Vector2 touchPosition)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_backgroundRect, touchPosition, _canvas.worldCamera, out _touchPosition))
        {
            _touchAxis.x = (_touchPosition.x / (_backgroundRect.sizeDelta.x * _handleBound));
            _touchAxis.y = (_touchPosition.y / (_backgroundRect.sizeDelta.y * _handleBound));
            _touchAxis = (_touchAxis.magnitude > 1.0f) ? _touchAxis.normalized : _touchAxis;

            _handleRect.anchoredPosition = new Vector2(
                _touchAxis.x * _backgroundRect.sizeDelta.x * _handleBound,
                _touchAxis.y * _backgroundRect.sizeDelta.y * _handleBound
            );
        }
    }

    private void OnEndTouch(Vector2 touchPosition)
    {
        _touchAxis = Vector2.zero;
        _handleRect.anchoredPosition = Vector2.zero;

        IsDrag = false;
        SetVisibleUI(IsDrag);
    }

    private void SetVisibleUI(bool isActive)
    {
        _background.SetActive(isActive);
        _handle.SetActive(isActive);
    }
}
