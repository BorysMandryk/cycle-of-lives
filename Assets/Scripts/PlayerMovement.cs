using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private LayerMask _jumpableLayer;

    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private Controls _controls;

    private float _moveValue;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        _controls = new Controls();

        _controls.Player.Move.performed += MoveReadValue;
        _controls.Player.Move.canceled += MoveReadValue;

        _controls.Player.Jump.performed += Jump;

        _controls.Player.Enable();
    }

    private void OnDisable()
    {
        _controls.Player.Move.performed -= MoveReadValue;
        _controls.Player.Move.canceled -= MoveReadValue;

        _controls.Player.Jump.performed -= Jump;

        _controls.Player.Disable();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 moveVelocity = new Vector2(_speed * _moveValue, _rigidbody.velocity.y);
        _rigidbody.velocity = moveVelocity;
    }

    private void MoveReadValue(InputAction.CallbackContext context)
    {
        _moveValue = context.ReadValue<float>();
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (IsGrounded())
        {
            Vector2 jumpVec = Vector2.up * _jumpForce;
            _rigidbody.AddForce(jumpVec, ForceMode2D.Impulse);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 0f, Vector2.down, 0.1f, _jumpableLayer);
    }
}
