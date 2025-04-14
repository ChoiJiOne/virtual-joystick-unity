using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private VirtualJoystick _virtualJoystick;
    [SerializeField] private float _moveSpeed;

    private Vector3 _moveDirection = Vector3.zero;

    private void Update()
    {
        _moveDirection.x = _virtualJoystick.Horizontal;
        _moveDirection.y = _virtualJoystick.Vertical;
        
        transform.position += _moveDirection * _moveSpeed * Time.deltaTime;
    }
}
