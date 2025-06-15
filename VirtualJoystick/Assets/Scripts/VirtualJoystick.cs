using UnityEngine;

public class VirtualJoystick : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TouchInputDispatcher _touchInputDispatcher;
    [SerializeField] private RectTransform _rootRect;
    [SerializeField] private GameObject _background;
    [SerializeField] private GameObject _handle;

    private RectTransform _canvasRect;

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
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvasRect, touchPosition, _canvas.worldCamera, out localPos);

        _rootRect.anchoredPosition = localPos;

        _background.SetActive(true);
        _handle.SetActive(true);
    }

    private void OnMoveTouch(Vector2 touchPosition)
    {

    }

    private void OnEndTouch(Vector2 touchPosition)
    {
        _background.SetActive(false);
        _handle.SetActive(false);
    }
}
