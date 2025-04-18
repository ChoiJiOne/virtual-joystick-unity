using UnityEngine;

public class BoundaryController : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GameObject _targetObject;

    private Bounds _bounds;

    private void Awake()
    {
        float cameraHeight = _mainCamera.orthographicSize * 2.0f;
        float cameraWidth = cameraHeight * _mainCamera.aspect;

        _bounds = new Bounds(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(cameraWidth, cameraHeight, 0.0f));
    }

    private void Update()
    {
        Vector3 targetPosition = _targetObject.transform.position;
        if (_bounds.Contains(targetPosition))
        {
            return;
        }

        targetPosition.x = Mathf.Clamp(targetPosition.x, _bounds.min.x, _bounds.max.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, _bounds.min.y, _bounds.max.y);

        _targetObject.transform.position = targetPosition;
    }
}
