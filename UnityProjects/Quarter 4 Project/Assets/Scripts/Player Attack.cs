using UnityEngine;
using System.Collections.Generic;

public class PlayerAttack : MonoBehaviour
{
    public float attackCooldown = 1f; // Time between attacks
    public float lastAttackTime = 0f; // Time of the last attack

    public int attackDamage = 10; // Damage dealt by the attack
    public bool isAttacking = false; // Flag to check if the player is currently attacking
    public NPCHealth enemyHealth;
    void Start()
    {
        isAttacking = false;


    }
    void Update()
    {
        // Check for mouse click to attack
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }
    public void Attack()
    {
     
        
            isAttacking = true; // Set attacking state to true when the attack is initiated
            
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
        

                if (isAttacking == true && Time.time - lastAttackTime >= attackCooldown)
                {
                    NPCHealth NPCHealth = collision.GetComponent<NPCHealth>();
                    if (NPCHealth != null)
                    {
                        NPCHealth.TakeDamage(attackDamage); // Example damage amount
                        Debug.Log("Player hit "+ collision.name + " with attack for " + attackDamage + " damage but cooldown is " + attackCooldown + " seconds" + " and last attack was at " + lastAttackTime + " seconds and current time is " + Time.time + " seconds, but also dont forget that the enemy has " + NPCHealth.currentHealth + " health remaining, but if the attack cooldown has not passed, the player cannot attack and the attack will not hit the enemy, but if the attack cooldown has passed, the player can attack and the attack will hit the enemy, but if the player is not currently attacking, the player cannot attack and the attack will not hit the enemy, and if the enemy health remaining is" + NPCHealth.currentHealth + " then the enemy is still alive, but if the enemy health remaining is 0 or less, then the enemy is dead!");
                        isAttacking = false; // Reset attack state after hitting the player
                    }
                    if(NPCHealth == null)
                    {
                        Debug.Log("HELP");
                        throw new System.Exception("HELP ITS BROKEN FIX IT PLEASE");
                    }
                    // Reset the attack cooldown
                    lastAttackTime = Time.time;
                    isAttacking = false; // Reset attacking state after the attack



        
                    
                        isAttacking = false; // Ensure attacking state is reset if cooldown hasn't passed
                    
                }
                 
            }
        }
    }





