using System;
// using System.Numerics;
using NUnit.Framework.Internal;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class PlayerController : MonoBehaviour
{
    private const string ACTION_MAP = "Player";
    // [SerializeField] private UIManager uI;
    [SerializeField] private InputActionAsset actions;
    private InputAction move;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpForce = 25f;
    private bool isJumping = false;
    private bool isCrouching = false;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    private Rigidbody2D rb;
    private Collider2D coll;
    [SerializeField] private int hpMax = 100;
    private int hp;
    // [SerializeField] private int hpLostByContact;
    [SerializeField] private int damage = 10;
    void OnEnable()
    {
        actions.FindActionMap(ACTION_MAP).Enable();
        actions.FindActionMap(ACTION_MAP).FindAction("Jump").performed += OnJump;
        actions.FindActionMap(ACTION_MAP).FindAction("Crouch").performed += OnCrouch;
        actions.FindActionMap(ACTION_MAP).FindAction("Crouch").canceled += OnStandUp;
    }
    void OnDisable()
    {
        actions.FindActionMap(ACTION_MAP).Disable();
        actions.FindActionMap(ACTION_MAP).FindAction("Jump").performed -= OnJump;
        actions.FindActionMap(ACTION_MAP).FindAction("Crouch").performed -= OnCrouch;
        actions.FindActionMap(ACTION_MAP).FindAction("Crouch").canceled -= OnStandUp;
    }
    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();

        move = actions.FindActionMap(ACTION_MAP).FindAction("Move");
        hp = hpMax;
    }
    public void Update()
    {
        MoveX();
        if (isJumping)
        {
            Vector3 origin = transform.position + Vector3.down * 0.9f;
            Vector3 direction = Vector3.down * 2f;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            Debug.Log("Touch floor");
            isJumping = false;
            animator.SetBool("on jump", false);
        }
    }


    private void MoveX()
    {
        Vector2 movement = move.ReadValue<Vector2>();
        spriteRenderer.flipX = movement.x < 0;

        if (isCrouching) return;
        // Debug.Log(speed + tmpSpeed);
        // transform.Translate(xAxis.ReadValue<float>() * (speed + tmpSpeed) * Time.deltaTime, 0f, 0f);
        transform.position += Vector3.right * (movement.x * speed) * Time.deltaTime;
        animator.SetFloat("speed", Math.Abs(movement.x));
    }
    private void OnJump(InputAction.CallbackContext callbackContext)
    {
        if (isJumping) return;
        animator.SetBool("on jump", true);
        isJumping = true;
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
    private void OnCrouch(InputAction.CallbackContext callbackContext)
    {
        speed *= 0.5f;
        animator.SetBool("on crouch", true);
        isCrouching = true;
    }
    private void OnStandUp(InputAction.CallbackContext callbackContext)
    {
        speed *= 2f;
        animator.SetBool("on crouch", false);
        isCrouching = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Boss")
        {
            // this.hp = collision.gameObject.damage;
        }

    }
}