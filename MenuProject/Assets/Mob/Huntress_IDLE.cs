using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Huntress_IDLE : StateMachineBehaviour
{
    // Reference to the Animator component
    private Animator animator;
    private GameObject player;
    private float detectionRange = 20f;

    // Start is called on the first frame of the animation
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        this.animator = animator;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player == null)
        {
            // Tìm game object có tag "Player"
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (player != null)
        {
            // Tính khoảng cách giữa animator và player
            float distanceToPlayer = Vector3.Distance(animator.transform.position, player.transform.position);

            // Nếu khoảng cách nhỏ hơn hoặc bằng detectionRange
            if (distanceToPlayer <= detectionRange)
            {
                // Kích hoạt animation "Run"
                animator.SetTrigger("Run"); // "Move" là tên parameter trigger trong Animator Controller
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Reset the trigger when exiting the state to prevent immediate re-entry
        animator.ResetTrigger("Run");
    }
}
