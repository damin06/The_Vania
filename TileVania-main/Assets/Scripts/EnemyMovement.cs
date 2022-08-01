using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    
    private CapsuleCollider2D _capsuleCollider;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        EnemyMove();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        speed = -speed;
        FlipSprite();
    }

    private void EnemyMove()
    {
        _rb.velocity = new Vector2(speed, 0f);
    }


    private void FlipSprite()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(_rb.velocity.x)), 1f); //Sign함수가 0이거나 양수이면 1로 반환
    }
}
