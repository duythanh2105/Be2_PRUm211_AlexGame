
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 9f;
    [SerializeField]
    private float jumpForce = 850;
    [SerializeField]
    private float attackDelay = 0.3f;

    private Animator animator;
    private Rigidbody2D rb2d;
    private bool isJumpPressed;
    private bool isAttackPressed;
    private bool isAttacking;
    private bool isGrounded;

    const string PLAYER_IDLE = "Player_Idle";
    const string PLAYER_RUN = "Player_Run";
    const string PLAYER_JUMP = "Player_Jumping";
    const string PLAYER_ATTACK = "Player_Attack";

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float xAxis = Input.GetAxisRaw("Horizontal");
        isJumpPressed = Input.GetKeyDown(KeyCode.Space);
        isAttackPressed = Input.GetKeyDown(KeyCode.RightControl);

        if (xAxis != 0)
        {
            transform.localScale = new Vector2(Mathf.Sign(xAxis), 1);
        }

        if (isJumpPressed)
        {
            rb2d.AddForce(new Vector2(0, jumpForce));
            ChangeAnimationState(PLAYER_JUMP);
            Debug.Log("Jumping");
            isJumpPressed = false;
        }
        // else if (isAttackPressed)
        // {
        //     ChangeAnimationState(PLAYER_ATTACK);
        //     isAttacking = true;
        //     Invoke("AttackComplete", attackDelay);
        // }
        else if (Mathf.Abs(xAxis) > 0.1f)
        {
            ChangeAnimationState(PLAYER_RUN);
            Debug.Log("Running");
        }
        // else
        // {
        //     ChangeAnimationState(PLAYER_IDLE);
        //     Debug.Log("Idle");

        // }
    }

    void FixedUpdate()
    {
        float xAxis = Input.GetAxisRaw("Horizontal");
        Vector2 vel = new Vector2(xAxis * walkSpeed, rb2d.velocity.y);
        rb2d.velocity = vel;

        // check isGround
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 0.1f, Color.red);
        isGrounded = (hit.collider != null);
        Debug.Log(isGrounded);
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

}






// public class PlayerController : MonoBehaviour
// {
//     [SerializeField]
//     private float walkSpeed = 9f;
//     [SerializeField]
//     private float jumpForce = 850;
//     [SerializeField]
//     private float attackDelay = 0.3f;

//     private Animator animator;
//     private Rigidbody2D rb2d;
//     private bool isJumpPressed;
//     private bool isAttackPressed;
//     private bool isAttacking;

//     const string PLAYER_IDLE = "Player_Idle";
//     const string PLAYER_RUN = "Player_Run";
//     const string PLAYER_JUMP = "Player_Jumping";
//     const string PLAYER_ATTACK = "Player_Attack";

//     void Start()
//     {
//         rb2d = GetComponent<Rigidbody2D>();
//         animator = GetComponent<Animator>();
//     }

//     void Update()
//     {
//         float xAxis = Input.GetAxisRaw("Horizontal");
//         isJumpPressed = Input.GetKeyDown(KeyCode.Space);
//         isAttackPressed = Input.GetKeyDown(KeyCode.RightControl);

//         if (xAxis != 0)
//         {
//             transform.localScale = new Vector2(Mathf.Sign(xAxis), 1);
//         }

//         if (isJumpPressed)
//         {
//             rb2d.AddForce(new Vector2(0, jumpForce));
//             isJumpPressed = false;
//             ChangeAnimationState(PLAYER_JUMP);
//             Debug.Log("nhay ne");
//         }
//         else if (isAttackPressed)
//         {
//             ChangeAnimationState(PLAYER_ATTACK);
//             isAttacking = true;
//             Invoke("AttackComplete", attackDelay);
//         }
//         else if (Mathf.Abs(xAxis) > 0.1f)
//         {
//             ChangeAnimationState(PLAYER_RUN);
//         }
//         else
//         {
//             ChangeAnimationState(PLAYER_IDLE);
//         }
//     }

//     void FixedUpdate()
//     {
//         float xAxis = Input.GetAxisRaw("Horizontal");
//         Vector2 vel = new Vector2(xAxis * walkSpeed, rb2d.velocity.y);
//         rb2d.velocity = vel;
//     }

//     void AttackComplete()
//     {
//         isAttacking = false;
//     }

//     void ChangeAnimationState(string newAnimation)
//     {
//         if (animator.GetCurrentAnimatorStateInfo(0).IsName(newAnimation)) return;
//         animator.Play(newAnimation);
//     }
// }




// [SerializeField]
// private float walkSpeed = 5f;

// private Animator animator;

// private float xAxis;
// private float yAxis;
// private Rigidbody2D rb2d;
// private bool isJumpPressed;
// private float jumpForce = 600;
// private int groundMask;
// private bool isGrounded;
// private string currentAnimaton;
// private bool isAttackPressed;
// private bool isAttacking;

// [SerializeField]
// private float attackDelay = 0.3f;

// //Animation States
// const string PLAYER_IDLE = "Player_Idle";
// const string PLAYER_RUN = "Player_Run";
// const string PLAYER_JUMP = "Player_Jumping";
// const string PLAYER_ATTACK = "Player_Attack";

// //=====================================================
// // Start is called before the first frame update
// //=====================================================
// void Start()
// {
//     rb2d = GetComponent<Rigidbody2D>();
//     animator = GetComponent<Animator>();
//     groundMask = 1 << LayerMask.NameToLayer("Ground");

// }

// //=====================================================
// // Update is called once per frame
// //=====================================================
// void Update()
// {
//     //Checking for inputs
//     xAxis = Input.GetAxisRaw("Horizontal");

//     //space jump key pressed?
//     if (Input.GetKeyDown(KeyCode.Space))
//     {
//         isJumpPressed = true;
//     }

//     //space Atatck key pressed?
//     if (Input.GetKeyDown(KeyCode.RightControl))
//     {
//         isAttackPressed = true;
//     }
// }

// //=====================================================
// // Physics based time step loop
// //=====================================================
// private void FixedUpdate()
// {
//     //check if player is on the ground
//     RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundMask);

//     if (hit.collider != null)
//     {
//         isGrounded = true;
//     }
//     else
//     {
//         isGrounded = false;
//     }

//     //------------------------------------------

//     //Check update movement based on input
//     Vector2 vel = new Vector2(0, rb2d.velocity.y);

//     if (xAxis < 0)
//     {
//         vel.x = -walkSpeed;
//         transform.localScale = new Vector2(-1, 1);
//     }
//     else if (xAxis > 0)
//     {
//         vel.x = walkSpeed;
//         transform.localScale = new Vector2(1, 1);

//     }
//     else
//     {
//         vel.x = 0;

//     }

//     if (isGrounded && !isAttacking)
//     {
//         if (xAxis != 0)
//         {
//             ChangeAnimationState(PLAYER_RUN);
//         }
//         else
//         {
//             ChangeAnimationState(PLAYER_IDLE);
//         }
//     }
//     //------------------------------------------

//     //Check if trying to jump 
//     if (isJumpPressed && isGrounded)
//     {
//         rb2d.AddForce(new Vector2(0, jumpForce));
//         isJumpPressed = false;
//         ChangeAnimationState(PLAYER_JUMP);
//     }

//     //assign the new velocity to the rigidbody
//     rb2d.velocity = vel;


//     //attack
//     if (isAttackPressed)
//     {
//         isAttackPressed = false;

//         if (!isAttacking)
//         {
//             isAttacking = true;

//             if (isGrounded)
//             {
//                 ChangeAnimationState(PLAYER_ATTACK);
//             }
//             // else
//             // {
//             //     ChangeAnimationState(PLAYER_AIR_ATTACK);
//             // }

//             Invoke("AttackComplete", attackDelay);
//         }
//     }
// }

// void AttackComplete()
// {
//     isAttacking = false;
// }

// //=====================================================
// // mini animation manager
// //=====================================================
// void ChangeAnimationState(string newAnimation)
// {
//     if (currentAnimaton == newAnimation) return;

//     animator.Play(newAnimation);
//     currentAnimaton = newAnimation;
// }