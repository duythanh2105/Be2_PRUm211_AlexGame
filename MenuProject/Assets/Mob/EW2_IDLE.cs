using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EW2_IDLE : StateMachineBehaviour
{
    // Reference to the Animator component
    private Animator animator;

    // Start is called on the first frame of the animation
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        this.animator = animator;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Check if there is a game object appearing (you can replace this condition with your own)
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            // Transition to the "RUN" animation state
            animator.SetTrigger("Move"); // "RunTrigger" should be a parameter in your Animator Controller
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Reset the trigger when exiting the state to prevent immediate re-entry
        animator.ResetTrigger("Move");
    }
}
