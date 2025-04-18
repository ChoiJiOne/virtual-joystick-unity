using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool IsRun => _isRun;

    [SerializeField] private VirtualJoystick _virtualJoystick;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private SpriteRenderer _shadowSpriteRenderer;

    private bool _isRun = false;
    private Vector3 _moveDirection = Vector3.zero;

    private void Update()
    {
        if (_virtualJoystick.IsDrag)
        {
            UpdateMoveDirection();
            transform.position += _moveDirection * _moveSpeed * Time.deltaTime;
        }

        UpdateSpriteFlip();
        UpdateAnimationState();
        UpdateShadowSprite();
    }

    private void UpdateMoveDirection()
    {
        _moveDirection.x = _virtualJoystick.Horizontal;
        _moveDirection.y = _virtualJoystick.Vertical;
    }

    private void UpdateSpriteFlip()
    {
        _spriteRenderer.flipX = _moveDirection.x < 0.0f;
    }
    
    private void UpdateAnimationState()
    {
        bool isDirty = false;
        if (_isRun && !_virtualJoystick.IsDrag)
        {
            isDirty = true;
            _isRun = false;
        }

        if (!_isRun && _virtualJoystick.IsDrag)
        {
            isDirty = true;
            _isRun = true;
        }

        if (isDirty)
        {
            _animator.SetBool(nameof(IsRun), _isRun);
        }
    }

    private void UpdateShadowSprite()
    {
        _shadowSpriteRenderer.sprite = _spriteRenderer.sprite;
        _shadowSpriteRenderer.flipX = _spriteRenderer.flipX;
    }
}
