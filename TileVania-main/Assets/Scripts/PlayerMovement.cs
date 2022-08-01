using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float jumppw = 10f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(20f, 20f);

    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;

    private Vector2 _moveInput;
    private Rigidbody2D _rb;
    private Animator _animator;
    private CapsuleCollider2D _capsuleCollider;
    private CircleCollider2D _circleCollider;

    private float gravityScaleAtStart;

    private bool _isDie = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        _circleCollider = GetComponent<CircleCollider2D>();

        gravityScaleAtStart = _rb.gravityScale;
    }

    private void Update()
    {
        if (_isDie == true) return;

        Run();
        FlipSprite();
        ClimbLadder();
        PlayerDie();
    }

    private void Run()
    {
        Vector2 _playerVelocity = new Vector2(_moveInput.x * speed, _rb.velocity.y);
        _rb.velocity = _playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(_rb.velocity.x) > Mathf.Epsilon;
        _animator.SetBool("IsRunning", playerHasHorizontalSpeed);
    }

    private void FlipSprite()
    {
        bool playerHasVerticalSpeed = Mathf.Abs(_rb.velocity.x) > Mathf.Epsilon;  //Epsilon은 0에 가장 가까운 숫자
        _animator.SetBool("IsCliming", false);

        if (playerHasVerticalSpeed)
            transform.localScale = new Vector2(Mathf.Sign(_rb.velocity.x), 1f); //Sign함수가 0이거나 양수이면 1로 반환
    }

    private void ClimbLadder()
    {
        if (!_capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            _rb.gravityScale = gravityScaleAtStart;
            _animator.SetBool("IsCliming", false);
            return;
        }

        Vector2 _climbVelocity = new Vector2(_rb.velocity.x, _moveInput.y * climbSpeed);
        _rb.velocity = _climbVelocity;
        _rb.gravityScale = 0f;

        bool playerVerticalSpeed = Mathf.Abs(_rb.velocity.y) > Mathf.Epsilon;
        _animator.SetBool("IsCliming", playerVerticalSpeed);
    }

    private void PlayerDie()
    {
        if (_capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Spikes")))
        {
            _animator.SetTrigger("IsDie");
            _isDie = true;
            _rb.velocity = deathKick;
            FindObjectOfType<gameSession>().PrecessPlayerDeath();
        }
    }

    private void OnMove(InputValue value)
    {
        if (_isDie == true) return;

        _moveInput = value.Get<Vector2>();
        _animator.SetBool("IsCliming", false);
    }

    private void OnJump(InputValue value)
    {
        if (_isDie == true) return;
        
        if (!_circleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            return;

        _animator.SetBool("IsCliming", false);
        
        if (value.isPressed)
            _rb.velocity += new Vector2(0f, jumppw);
    }

    private void OnFire(InputValue value)
    {
        if (_isDie == true) return;

        Instantiate(bullet, gun.position, transform.rotation);
    }
}
