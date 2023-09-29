
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 9f;
    [SerializeField]
    private float jumpForce = 10f;
    [SerializeField]
    private float attackDelay = 0.7f;

    public Transform attackPoint;
    private Animator animator;
    private Rigidbody2D rb2d;
    private bool isJumpPressed;
    private bool isAttackPressed;
    private bool isAttacking;
    public bool isGrounded;
    private float xAxis;
    public float attackRange = 0.5f;
    public int attackDamage = 40;
    public LayerMask enemyLayers;

    const string PLAYER_IDLE = "Player_Idle";
    const string PLAYER_RUN = "Player_Run";
    const string PLAYER_JUMP = "Player_Jumping";
    const string PLAYER_Falling = "Player_Falling";
    const string PLAYER_ATTACK = "Player_Attack";

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            ChangeAnimationState(PLAYER_JUMP);
            isJumpPressed = true;
        }

        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            isAttackPressed = true;
        }
    }

    private void FixedUpdate()
    {
        Vector2 vel = new Vector2(0, rb2d.velocity.y);

        if (xAxis < 0)
        {
            // if (!isAttacking)
            // {
            vel.x = -walkSpeed;
            transform.localScale = new Vector2(-1, 1);
            // }
        }
        else if (xAxis > 0)
        {
            // if (!isAttacking)
            // {
            vel.x = walkSpeed;
            transform.localScale = new Vector2(1, 1);
            // }
        }
        else
        {
            vel.x = 0;

        }

        if (isGrounded && !isAttacking)
        {
            if (xAxis != 0)
            {
                ChangeAnimationState(PLAYER_RUN);
            }
            else
            {
                ChangeAnimationState(PLAYER_IDLE);
            }
        }

        if (isJumpPressed && isGrounded)
        {
            ChangeAnimationState(PLAYER_JUMP);
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumpPressed = false;
        }

        if (isAttackPressed)
        {
            isAttackPressed = false;
            if (!isAttacking)
            {
                isAttacking = true;
                if (isGrounded)
                {
                    ChangeAnimationState(PLAYER_ATTACK);
                    Collider2D[] hitEnimies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
                    foreach (Collider2D enemy in hitEnimies)
                    {
                        enemy.GetComponent<MobController>().TakeDamage(attackDamage);
                    }
                }
                Invoke("AttackComplete", attackDelay);
            }
        }

        rb2d.velocity = vel;
    }

    void AttackComplete()
    {
        isAttacking = false;
    }

    void ChangeAnimationState(string newAnimation)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(newAnimation)) return;
        animator.Play(newAnimation);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    private void OnDrawGizmosSelected() {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
