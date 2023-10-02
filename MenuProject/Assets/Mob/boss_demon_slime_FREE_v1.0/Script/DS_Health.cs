using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DS_Health : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    int health;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }
    public void TakeDamage(int damage){
        health -= damage;

        if(health <= 0){
            Death();
        }
    }
    public void Death(){
        animator.SetBool("Death", true);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
