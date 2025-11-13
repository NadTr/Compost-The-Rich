using System;
using System.Collections;

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
    [Header("Movement")]
    private const string ACTION_MAP = "Player";
    // [SerializeField] private UIManager uI;
    [SerializeField] private InputActionAsset actions;
    private InputAction move;
    private float speed = 3f;
    private bool isJumping = false;
    private int numberOfJumps = 2;
    private bool isCrouching = false;
    private bool frontDirectionRight = false;

    [Space]
    [Header("Animation")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    private Rigidbody2D rb;
    private Collider2D coll;

    [Space]
    [Header("PlayerData")]
    [SerializeField] private PlayerData playerData;


    private int hp;
    GameObject attack;
    bool isAttackActive;
    
    void OnEnable()
    {
        actions.FindActionMap(ACTION_MAP).Enable();
        actions.FindActionMap(ACTION_MAP).FindAction("Jump").performed += OnJump;
        actions.FindActionMap(ACTION_MAP).FindAction("Attack").performed += Attack;
        actions.FindActionMap(ACTION_MAP).FindAction("Crouch").performed += OnCrouch;
        actions.FindActionMap(ACTION_MAP).FindAction("Crouch").canceled += OnStandUp;
    }
    void OnDisable()
    {
        actions.FindActionMap(ACTION_MAP).Disable();
        actions.FindActionMap(ACTION_MAP).FindAction("Jump").performed -= OnJump;
        actions.FindActionMap(ACTION_MAP).FindAction("Attack").performed -= Attack;
        actions.FindActionMap(ACTION_MAP).FindAction("Crouch").performed -= OnCrouch;
        actions.FindActionMap(ACTION_MAP).FindAction("Crouch").canceled -= OnStandUp;
    }
    public void Start()
    {
        speed = playerData.speed;

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        attack = transform.GetChild(0).gameObject;

        move = actions.FindActionMap(ACTION_MAP).FindAction("Move");
        hp = playerData.hpMax;
        isAttackActive = false;
    }
    public void Update()
    {
        MoveX();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            numberOfJumps = 2;
            animator.SetBool("on jump", false);
        }
    }


    private void MoveX()
    {
        Vector2 movement = move.ReadValue<Vector2>();

        frontDirectionRight = movement.x > 0f ? false : (movement.x < 0f ? true : frontDirectionRight);
        spriteRenderer.flipX = frontDirectionRight;

        if (isCrouching) return;

        transform.position += Vector3.right * (movement.x * speed) * Time.deltaTime;
        animator.SetFloat("speed", Math.Abs(movement.x));
    }
    private void OnJump(InputAction.CallbackContext callbackContext)
    {
        if (numberOfJumps <= 0) return;

        if (numberOfJumps > 1) rb.AddForce(transform.up * playerData.firstJumpForce, ForceMode2D.Impulse);
        if (numberOfJumps <= 1) rb.AddForce(transform.up * playerData.secondJumpForce, ForceMode2D.Impulse);
        
        animator.SetBool("on jump", true);
        numberOfJumps--;
    }
    private void Attack(InputAction.CallbackContext callbackContext)
    {
        animator.SetBool("attack", true);
        // StartCoroutine(ActiveAttack(0.3f));
        Invoke("ActiveAttackHitBox", 0.2f);
        Invoke("ActiveAttackHitBox", 1.5f);
    }
    private void ActiveAttackHitBox()
    {
        isAttackActive = !isAttackActive;
        attack.SetActive(isAttackActive);
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
            // this.hp -= collision.gameObject.damage;
        }

    }
}