using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobController : MonoBehaviour
{
    public Animator animator;
    public Transform player;
    public float walkDistance = 10f;
    public float walkSpeed = 2f;
    public float attackDistance = 2f;

    private bool rebornToIdleComplete;
    private bool canMove = true;

    public int maxHealth = 100;
    int currentHealth;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = maxHealth;
    }

    void Update()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;

        if (direction.x > 0)
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1);
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1.5f, 1.5f, 1);
        }

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < walkDistance && !rebornToIdleComplete)
        {
            if (OnRebornToIdleComplete())
            {
                if (canMove)
                {
                    animator.SetBool("IsWalking", true);
                    transform.Translate(direction * walkSpeed * Time.deltaTime, Space.World);
                }
            }
            rebornToIdleComplete = false;
            if (distance < attackDistance)
            {
                canMove = false;
                animator.SetBool("IsWalking", false);
                animator.SetTrigger("ToAttack");
            }
            else
            {
                canMove = true;
                animator.SetBool("IsWalking", true);
            }
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }

    public bool OnRebornToIdleComplete()
    {
        animator.SetTrigger("IntroToReborn");
        animator.SetTrigger("RebornToIdle");
        return rebornToIdleComplete = true;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hurt");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // animator.SetBool("IsDie", true);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        Debug.Log("Enemy die");
    }
}
