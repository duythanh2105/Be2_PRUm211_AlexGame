
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
    public bool isJump;
    private float xAxis;
    public float attackRange = 0.5f;
    public int attackDamage = 40;
    public int maxPlayerHealth = 150;
    int currentPlayerHealth;
    public LayerMask enemyLayers;

    const string PLAYER_IDLE = "Player_Idle";
    const string PLAYER_RUN = "Player_Run";
    const string PLAYER_JUMP = "Player_Jumping";
    const string PLAYER_Falling = "Player_Falling";
    const string PLAYER_ATTACK = "Player_Attack";
    const string PLAYER_HURT = "Player_Hurt";
    const string PLAYER_DEAD = "Player_Dead";

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentPlayerHealth = maxPlayerHealth;
    }

    void Update()
    {
        Debug.Log("Ground: " + isGrounded);

        xAxis = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isJumpPressed = true;
        }

        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            isAttackPressed = true;
        }
        JumpFall();
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

        if (isGrounded && !isAttacking && !isJumpPressed)
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
                        Debug.Log("we hit " + enemy.name);
                        enemy.GetComponent<MobController>().TakeDamage(attackDamage);
                    }


                }
                Invoke("AttackComplete", attackDelay);
            }
        }

        rb2d.velocity = vel;
    }

    //check attack complete
    void AttackComplete()
    {
        isAttacking = false;
    }

    //change animation
    void ChangeAnimationState(string newAnimation)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(newAnimation)) return;
        animator.Play(newAnimation);
    }

    //check Ground
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    //check Ground
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    //attack point
  private void OnDrawGizmosSelected()
{
    if (attackPoint == null) return;

    // Đặt màu gizmo
    Gizmos.color = Color.red;

    // Vẽ hình tròn tại attackPoint với attackRange
    Gizmos.DrawWireSphere(attackPoint.position, attackRange);
}

    void JumpFall()
    {
        if (isJumpPressed && isGrounded)
        {
            Debug.Log("Press Jump" + isJumpPressed);
            // ChangeAnimationState(PLAYER_JUMP);
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetBool("IsJumping", true);
            isJumpPressed = false;
        }
        if (!isGrounded)
        {
            // Debug.Log("Press Jump: " + isJumpPressed);
            Debug.Log("Ground falling: " + isGrounded);
            animator.SetBool("IsFalling", true);
            animator.SetBool("IsJumping", false);
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("damage: " + damage);
        currentPlayerHealth -= damage;
        Debug.Log("currentPlayerHealth: " + currentPlayerHealth);
        ChangeAnimationState(PLAYER_HURT);
        if (currentPlayerHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // animator.SetBool("IsDie", true);
        ChangeAnimationState(PLAYER_DEAD);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        // GetComponent<SpriteRenderer>().enabled = false;
        Debug.Log("Player die");
    }

}
