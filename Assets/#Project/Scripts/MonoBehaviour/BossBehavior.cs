using System;
// using System.Numerics;
using UnityEditor.EditorTools;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BossBehavior : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float jumpForce = 35f;
    [SerializeField] private float jumpEveryXSeconds = 4f;
    [SerializeField] private bool goRight = true;
    [SerializeField] private int hpMax = 100;
    private int hp;
    [SerializeField] private int damage = 10;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        hp = hpMax;
    }
    void Update()
    {
        Move();


    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Enemy")
        {
            InverseSpeed();
        }
    }

    private void InverseSpeed()
    {
        goRight = !goRight;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }
    private void Move()
    {
        transform.Translate((goRight ? 1f : -1f) * speed * Time.deltaTime, 0f, 0f);
    }
}