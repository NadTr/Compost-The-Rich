using System;
// using System.Numerics;
using UnityEditor.EditorTools;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BossBehaviorState : MonoBehaviour
{

    [Space]
    [Header("GameObjects")]
    [SerializeField] private Transform player;

    [Header("Movement")]
    private float chrono;
    private float speed;
    [SerializeField] private float speedMin = 2f;
    [SerializeField] private float speedMax = 6f;
    private float jumpForce;
    [SerializeField] private float jumpForceMin = 15f;
    [SerializeField] private float jumpForceMax = 50f;
    private string[] allBehaviors = { "walk", "slow_attack", "jump_attack", "fast_attack"};
    private string state;
    private float stateChangeEveryXSeconds = 3f;
    [SerializeField] private float stateChangeEveryXSecondsMin = 3f;
    [SerializeField] private float stateChangeEveryXSecondsMax = 7f;
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
        stateChangeEveryXSeconds = stateChangeEveryXSecondsMin;
        hp = hpMax;
    }
    void Update()
    {

        Move();

        chrono += Time.deltaTime;

        if (chrono >= stateChangeEveryXSeconds)
        {
            chrono = 0;
            stateChangeEveryXSeconds = UnityEngine.Random.Range(stateChangeEveryXSecondsMin, stateChangeEveryXSecondsMax);

            state = allBehaviors[UnityEngine.Random.Range(0, allBehaviors.Length)];
            Debug.Log($"state = {state}");
            switch (state)
        {
            case "walk":
                Move();
                break;
            case "slow_attack":
                ChangeSpeed();
                break;
            case "jump_attack":
                Jump();
                break;
            case "fast_attack":
                ChangeSpeed();
                break;
            default:
                Move();
                break;
        }
        }

        Vector3 origin = transform.position + Vector3.up * 1.4f + (goRight ? 1f : -1f) * 0.5f * Vector3.right;
        Vector3 direction = (goRight ? 1f : -1f) * Vector3.right;
        RaycastHit2D sideHit = Physics2D.Raycast(origin, direction, 0.2f);
        Debug.DrawRay(origin, direction, Color.cyan);

        if (sideHit.collider != null && sideHit.collider.gameObject.tag == "Wall") InverseSpeed();

    }

    public void LookAtPlayer()
	{
		if (transform.position.x > player.position.x && !spriteRenderer.flipX)
		{
			InverseSpeed();
		}
		else if (transform.position.x < player.position.x && spriteRenderer.flipX)
		{
			InverseSpeed();
		}
	}
    private void Move()
    {
        animator.SetFloat("speed", Math.Abs(speed));
        transform.Translate((goRight ? 1f : -1f) * speed * Time.deltaTime, 0f, 0f);
    }
    private void InverseSpeed()
    {
        goRight = !goRight;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    private void ChangeSpeed()
    {
        LookAtPlayer();
        speed = UnityEngine.Random.Range(speedMin, speedMax);
    }

    private void Jump()
    {
        LookAtPlayer();
        jumpForce = UnityEngine.Random.Range(jumpForceMin, jumpForceMax);

        animator.SetBool("on jump", true);
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

}