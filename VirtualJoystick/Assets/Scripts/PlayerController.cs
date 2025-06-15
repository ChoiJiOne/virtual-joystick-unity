using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private VirtualJoystick _joyStick;
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private float _moveSpeed = 8.0f;

    private void Update()
    {
        Vector3 newVelocity = new Vector3(_joyStick.Axis.x, 0.0f, _joyStick.Axis.y);
        newVelocity *= _moveSpeed;
        _rigidBody.linearVelocity = newVelocity;
    }
}
