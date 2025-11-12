using System;
// using System.Numerics;
using UnityEditor.EditorTools;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BossBehavior : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private float jumpForceMin = 15f;
    [SerializeField] private float jumpForceMax = 50f;
    [SerializeField] private float jumpEveryXSecondsMin = 3f;
    [SerializeField] private float jumpEveryXSecondsMax = 7f;
    [SerializeField] private bool goRight = true;

    [Space]
    [Header("Animation")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    private Rigidbody2D rb;
    private Collider2D coll;
    
    [Space]
    [Header("Health")]
    private int hp;
    [SerializeField] private int hpMax = 100;
    [SerializeField] private int damage = 10;
    
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