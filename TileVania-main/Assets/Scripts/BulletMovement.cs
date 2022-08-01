using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 5f;

    private Rigidbody2D _rb;
    private CapsuleCollider2D _capsuleCollider;
    private GameObject _player;

    private float _xspeed;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _capsuleCollider = GetComponent<CapsuleCollider2D>();

        _player = GameObject.FindWithTag("Player");
    }

    private void Start()
    {
        _xspeed = _player.transform.localScale.x * bulletSpeed;
    }

    private void Update()
    {
        BulletMove();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Player")) && !_capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            Destroy(gameObject);
        }
    }

    private void BulletMove()
    {
        _rb.velocity = new Vector2(_xspeed, 0);
    }

}
