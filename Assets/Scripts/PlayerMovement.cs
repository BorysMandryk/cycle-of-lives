using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{

    [Header("Movement Config")]
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private LayerMask _jumpableLayer;

    private Rigidbody2D _rigidbody;
    private Collider2D _collider;

    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        Move();

        if (IsGrounded() && _playerInput.JumpPressed)
        {
            Jump();
        }
    }

    private void Move()
    {
        Vector2 moveVelocity = new Vector2(_speed * _playerInput.MoveValue, _rigidbody.velocity.y);
        _rigidbody.velocity = moveVelocity;
    }

    private void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0f);

        Vector2 jumpVec = Vector2.up * _jumpForce;
        _rigidbody.AddForce(jumpVec, ForceMode2D.Impulse);
    }

    private bool IsGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size * 0.85f, 0f, Vector2.down, 0.1f, _jumpableLayer);

        if (hitInfo.normal.sqrMagnitude < Mathf.Epsilon)
        {
            return false;
        }
        
        float vecAngle = Vector2.Angle(hitInfo.normal, Vector2.up);

        return vecAngle < 1;
    }
}
