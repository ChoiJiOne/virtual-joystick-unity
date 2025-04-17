using UnityEngine;

public class BoundaryController : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;

    private float _cameraWidth = 0.0f;
    private float _cameraHeight = 0.0f;
    
    private void Awake()
    {
        _cameraHeight = _mainCamera.orthographicSize * 2.0f;
        _cameraWidth = _cameraHeight * _mainCamera.aspect;
    }
}
