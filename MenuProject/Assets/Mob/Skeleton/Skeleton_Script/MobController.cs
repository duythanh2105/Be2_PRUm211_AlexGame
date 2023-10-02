// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class MobController : MonoBehaviour
// {
//     public Transform mobAttackPoint;
//     public Animator animator;
//     public Transform player;
//     public float walkDistance = 10f;
//     public float walkSpeed = 2f;
//     public float attackDistance = 2f;
//     public float MobAttackRange = 1.3f;
//     public int mobAttackDamage = 10;
//     public LayerMask playerLayers;

//     private bool rebornToIdleComplete;
//     private bool canMove = true;

//     public int maxMobHealth = 100;
//     int currenMobtHealth;

//     void Start()
//     {
//         animator = GetComponent<Animator>();
//         player = GameObject.FindGameObjectWithTag("Player").transform;
//         currenMobtHealth = maxMobHealth;
//     }

//     void Update()
//     {
//         Vector3 direction = (player.transform.position - transform.position).normalized;

//         if (direction.x > 0)
//         {
//             transform.localScale = new Vector3(1.5f, 1.5f, 1);
//         }
//         else if (direction.x < 0)
//         {
//             transform.localScale = new Vector3(-1.5f, 1.5f, 1);
//         }

//         float distance = Vector3.Distance(transform.position, player.position);
//         if (distance < walkDistance && !rebornToIdleComplete)
//         {
//             if (OnRebornToIdleComplete())
//             {
//                 if (canMove)
//                 {
//                     animator.SetBool("IsWalking", true);
//                     transform.Translate(direction * walkSpeed * Time.deltaTime, Space.World);
//                 }
//             }
//             rebornToIdleComplete = false;
//             if (distance < attackDistance)
//             {
//                 canMove = false;
//                 animator.SetBool("IsWalking", false);
//                 animator.SetTrigger("ToAttack");
//                 Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(mobAttackPoint.position, MobAttackRange, playerLayers);
//                 foreach (Collider2D character in hitPlayer)
//                 {
//                     character.GetComponent<PlayerController>().TakeDamage(mobAttackDamage);
//                     Debug.Log("Attack cua boss voi " + character.name);
//                 }
//             }
//             else
//             {
//                 canMove = true;
//                 animator.SetBool("IsWalking", true);
//             }
//         }
//         else
//         {
//             animator.SetBool("IsWalking", false);
//         }
//     }

//     public bool OnRebornToIdleComplete()
//     {
//         animator.SetTrigger("IntroToReborn");
//         animator.SetTrigger("RebornToIdle");
//         return rebornToIdleComplete = true;
//     }

//     //attack point
//     private void OnDrawGizmosSelected()
//     {
//         if (mobAttackPoint == null) return;
//         Gizmos.DrawWireSphere(mobAttackPoint.position, mobAttackDamage);
//     }


//     public void TakeDamage(int damage)
//     {
//         Debug.Log("Damage cua boss");
//         currenMobtHealth -= damage;
//         animator.SetTrigger("Hurt");
//         if (currenMobtHealth <= 0)
//         {
//             Die();
//         }
//     }

//     private void Die()
//     {
//         // animator.SetBool("IsDie", true);
//         GetComponent<Collider2D>().enabled = false;
//         this.enabled = false;
//         GetComponent<SpriteRenderer>().enabled = false;
//         Debug.Log("Enemy die");
//     }
// }
