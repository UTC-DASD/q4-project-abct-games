using UnityEngine;
using System.Collections.Generic;

public class PlayerAttack : MonoBehaviour
{
    public float attackCooldown = 1f; // Time between attacks
    public float lastAttackTime = 0f; // Time of the last attack
    public int attackDamage = 10; // Damage dealt by the attack
    public bool isAttacking = false; // Flag to check if the player is currently attacking
    public readonly List<NPCHealth> enemiesInRange = new List<NPCHealth>();
    public bool whyIsThisNotWorkingAtAllHelpMe;

    void Start()
    {
        lastAttackTime = -attackCooldown;
        isAttacking = false;
        lastAttackTime = 0f;
        Debug.Log("PlayerAttack Start: cooldown=" + attackCooldown + ", damage=" + attackDamage + ", lastAttackTime=" + lastAttackTime);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Attack input detected at time " + Time.time + ". isAttacking=" + isAttacking + ", enemiesInRange=" + enemiesInRange.Count);
            Attack();
        }
    }

    public void Attack()
    {
        Debug.Log("Attempting Attack: time=" + Time.time + ", lastAttackTime=" + lastAttackTime + ", cooldown=" + attackCooldown);

        if (Time.time - lastAttackTime < attackCooldown)
        {
            Debug.Log("Attack canceled: still on cooldown. Time since last attack=" + (Time.time - lastAttackTime));
            return;
        }

        enemiesInRange.RemoveAll(enemy => enemy == null);
        Debug.Log("Enemy list cleaned: enemiesInRange=" + enemiesInRange.Count);

        if (enemiesInRange.Count == 0)
        {
            Debug.Log("Attack canceled: no enemies in range.");
            return;
        }

        isAttacking = true;
        Debug.Log("Beginning attack on " + enemiesInRange.Count + " enemies.");

        foreach (NPCHealth enemy in enemiesInRange)
        {
            if (enemy != null)
            {
                Debug.Log("Damaging enemy " + enemy.name + " with currentHealth=" + enemy.currentHealth + ", damage=" + attackDamage);
                enemy.TakeDamage(attackDamage);
                Debug.Log("Enemy " + enemy.name + " health after attack=" + enemy.currentHealth);
            }
            else
            {
                Debug.Log("Skipped null enemy reference in enemiesInRange.");
            }
        }

        lastAttackTime = Time.time;
        Debug.Log("Attack finished. lastAttackTime updated to " + lastAttackTime);
        isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D: " + collision.name + " tag=" + collision.tag);
        if (collision.CompareTag("Enemy"))
        {
            NPCHealth enemyHealth = collision.GetComponent<NPCHealth>();
            if (enemyHealth != null)
            {
                if (!enemiesInRange.Contains(enemyHealth))
                {
                    enemiesInRange.Add(enemyHealth);
                    Debug.Log("Enemy added: " + enemyHealth.name + ". Total enemies=" + enemiesInRange.Count);
                }
                else
                {
                    Debug.Log("Enemy already in range: " + enemyHealth.name);
                }
            }
            else
            {
                Debug.Log("Enemy trigger had no NPCHealth component: " + collision.name);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       
        if (collision.CompareTag("Enemy"))
        {
             Debug.Log("OnTriggerExit2D: " + collision.name + " tag=" + collision.tag);
            NPCHealth enemyHealth = collision.GetComponent<NPCHealth>();
            if (enemyHealth != null)
            {
                enemiesInRange.Remove(enemyHealth);
                Debug.Log("Enemy removed: " + enemyHealth.name + ". Total enemies=" + enemiesInRange.Count);
            }
            else
            {
                Debug.Log("Enemy exit had no NPCHealth component: " + collision.name);
            }
        }
    }
}

