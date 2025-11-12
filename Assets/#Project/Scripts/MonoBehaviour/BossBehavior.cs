using System;
// using System.Numerics;
using UnityEditor.EditorTools;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BossBehavior : MonoBehaviour
{
    [Header("Movement")]
    private float chrono;
    private float speed;
    [SerializeField] private float speedMin = 2f;
    [SerializeField] private float speedMax = 6f;
    private float jumpForce;
    [SerializeField] private float jumpForceMin = 15f;
    [SerializeField] private float jumpForceMax = 50f;
    private float jumpEveryXSecond = 3f;
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
        rb = GetComponent<Rigidbody2D>();

        chrono = 0;
        speed = speedMin;
        jumpForce = jumpForceMin;
        jumpEveryXSecond = jumpEveryXSecondsMin;
        hp = hpMax;
    }
    void Update()
    {
        Move();

        chrono += Time.deltaTime;
        
        if(chrono >= jumpEveryXSecond)
        {
            Jump();
            Debug.Log("OnJump");
            Debug.Log(chrono);
        }
        if(chrono >= 4)
        {
            ChangeSpeed();
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
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
        animator.SetFloat("speed", Math.Abs(speed));
        transform.Translate((goRight ? 1f : -1f) * speed * Time.deltaTime, 0f, 0f);
    }

    private void ChangeSpeed()
    { 
        speed = UnityEngine.Random.Range(speedMin, speedMax);
    }

    private void Jump()
    {
        chrono = 0;
        jumpForce = UnityEngine.Random.Range(jumpForceMin, jumpForceMax);
        jumpEveryXSecond = UnityEngine.Random.Range(jumpEveryXSecondsMin, jumpEveryXSecondsMax);

        animator.SetBool("on jump", true);
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
}