using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    [Header("Movement Config")]
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _acceleration = 5f;
    [SerializeField] private float _decceleration = 5f;

    [SerializeField] private float _frictionAmount = 0.2f;

    [Header("Jump Config")]
    [Space]

    [SerializeField] private float _jumpHeight = 3f;
    [SerializeField] private float _gravityScale = 3f;
    [SerializeField] private float _gravityFallScale = 5f;
    [SerializeField] private LayerMask _jumpableLayer;

    [Header("Particles")]
    [Space]
    [SerializeField] private ParticleSystem _movePS;
    [SerializeField] private ParticleSystem _landPS;

    [Header("SFX")]
    [SerializeField] private AudioClip _jumpSFX;

    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private AudioSource _audioSource;

    private float _moveDir;
    private bool _groundedPrevFrame = true;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _inputReader.MoveEvent += HandleMove;
        _inputReader.JumpEvent += Jump;
    }

    private void OnDisable()
    {
        _inputReader.MoveEvent -= HandleMove;
        _inputReader.JumpEvent -= Jump;
    }

    private void Update()
    {
        Face();
        Land();
    }

    private void FixedUpdate()
    {
        ApplyGravity();
        Move();
        ApplyFriction();
    }

    private void HandleMove(float moveDir)
    {
        _moveDir = moveDir;
    }

    private void Face()
    {
        if (_moveDir == 0)
        {
            return;
        }

        Vector3 playerScale = transform.localScale;
        playerScale.x = _moveDir;
        transform.localScale = playerScale;
    }

    private void Move()
    {
        MoveParticles();
        float targetSpeed = _moveDir * _speed;
        float speedDif = targetSpeed - _rigidbody.velocity.x;

        float accelRate = (Mathf.Abs(targetSpeed) > Mathf.Epsilon) ? _acceleration : _decceleration;
        float movement = Mathf.Abs(speedDif) * accelRate * Mathf.Sign(speedDif);

        _rigidbody.AddForce(movement * Vector2.right);
    }

    private void MoveParticles()
    {
        if (_moveDir != 0 && IsGrounded())
        {
            _movePS.Play();
        }
        else
        {
            _movePS.Stop();
        }
    }

    private void ApplyFriction()
    {
        if (Mathf.Abs(_moveDir) < 0.01f)
        {
            float amount = Mathf.Min(Mathf.Abs(_rigidbody.velocity.x), Mathf.Abs(_frictionAmount));
            amount *= Mathf.Sign(_rigidbody.velocity.x);
            _rigidbody.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
    }

    private void ApplyGravity()
    {
        if (_rigidbody.velocity.y < -0.01)
        {
            _rigidbody.gravityScale = _gravityFallScale;
        }
        else
        {
            _rigidbody.gravityScale = _gravityScale;
        }
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            _rigidbody.gravityScale = _gravityScale;
            float jumpForce = Utils.HeightToForce(_jumpHeight, _rigidbody);
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            _audioSource.PlayOneShot(_jumpSFX);
        }
    }

    //private void Jump()
    //{
    //    if (IsGrounded())
    //    {
    //        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0f);

    //        Vector2 jumpVec = Vector2.up * _jumpForce;
    //        _rigidbody.AddForce(jumpVec, ForceMode2D.Impulse);
    //    }
    //}

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

    private void Land()
    {
        if (!_groundedPrevFrame && IsGrounded())
        {
            _landPS.Play();
        }
        _groundedPrevFrame = IsGrounded();
    }
}
