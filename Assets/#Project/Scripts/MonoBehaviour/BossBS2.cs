using System;
using System.Collections;
// using UnityEditor.EditorTools;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BossBS2 : MonoBehaviour
{

    [Space]
    [Header("GameObjects")]
    [SerializeField] private Transform player;
    [SerializeField] private GameObject tesla;


    [Space]
    [Header("Animation")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;

    [Space]
    [Header("BossData")]
    [SerializeField] private BossData bossData;


    // [Header("Movement")]
    private float chrono;
    private float speed;
    private float jumpForce;
    private string[] allBehaviors = { "walk", "slow_attack", "jump_attack", "fast_attack", "normal" };
    private string state;
    private float stateChangeEveryXSeconds = 3f;
    private bool goRight;
    private bool stopping = false;

    private Rigidbody2D rb;
    private Collider2D coll;

    private int hp;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        this.goRight = bossData.goRight;

        chrono = 0;
        speed = bossData.speedMin;
        jumpForce = bossData.jumpForceMin;
        stateChangeEveryXSeconds = bossData.stateChangeEveryXSecondsMin;
        hp = bossData.maxHealth;
    }
    void Update()
    {
        if (!stopping) Move();

        chrono += Time.deltaTime;

        Vector3 origin = transform.position + (goRight ? 1f : -1f) * 0.5f * Vector3.right;
        Vector3 direction = (goRight ? 1f : -1f) * Vector3.right;
        RaycastHit2D sideHit = Physics2D.Raycast(origin, direction, 0.8f);
        // Debug.DrawRay(origin, direction, Color.cyan);

        if (sideHit.collider != null && sideHit.collider.gameObject.tag != "Wall" && sideHit.collider.gameObject.tag == "Player")
        {
            state = "slow_attack";
            tesla.SetActive(true);
            StartCoroutine(SlowAttack());
        }
        else if (chrono >= stateChangeEveryXSeconds)
        {
            chrono = 0;
            stateChangeEveryXSeconds = UnityEngine.Random.Range(bossData.stateChangeEveryXSecondsMin, bossData.stateChangeEveryXSecondsMax);
            state = allBehaviors[UnityEngine.Random.Range(0, allBehaviors.Length)];

            // Debug.Log($"state = {state}");
            switch (state)
            {
                case "walk":
                    Move();
                    break;
                case "slow_attack":
                    tesla.SetActive(true);
                    StartCoroutine(SlowAttack());
                    break;
                case "jump_attack":
                    Jump();
                    break;
                case "fast_attack":
                    ChangeSpeed();
                    break;
                case "normal":
                    Move();
                    break;
            }
        }

        if (sideHit.collider != null && sideHit.collider.gameObject.tag == "Wall") InverseSpeed();

    }

    public void LookAtPlayer()
    {
        if (transform.position.x > player.position.x && spriteRenderer.flipX)
        {
            InverseSpeed();
        }
        else if (transform.position.x < player.position.x && !spriteRenderer.flipX)
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
        speed = UnityEngine.Random.Range(bossData.speedMin, bossData.speedMax);
    }

    private void Jump()
    {
        LookAtPlayer();
        jumpForce = UnityEngine.Random.Range(bossData.jumpForceMin, bossData.jumpForceMax);

        animator.SetBool("on jump", true);
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private IEnumerator SlowAttack()
    {
        stopping = true;
        // Debug.Log("stopping = " + stopping);
        yield return new WaitForSeconds(2.2f);
        stopping = false;
    }

    public int GetDamage()
    {
        return bossData.damage;
    }
    

}
