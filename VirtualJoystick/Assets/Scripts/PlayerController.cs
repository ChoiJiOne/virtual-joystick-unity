using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private VirtualJoystick _virtualJoystick;

    private Vector3 _moveDirection = Vector3.zero;
    private float _moveSpeed = 1.0f;

    private void Update()
    {
        _moveDirection.x = _virtualJoystick.Horizontal;
        _moveDirection.y = _virtualJoystick.Vertical;

        transform.position += _moveDirection * _moveSpeed * Time.deltaTime;
    }
}
