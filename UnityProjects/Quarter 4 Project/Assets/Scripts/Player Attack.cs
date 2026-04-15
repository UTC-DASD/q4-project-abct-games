using UnityEngine;
using System.Collections.Generic;

public class PlayerAttack : MonoBehaviour
{
    public float attackCooldown = 1f; // Time between attacks
    public float lastAttackTime = 0f; // Time of the last attack
    public int attackDamage = 10; // Damage dealt by the attack
    public bool isAttacking = false; // Flag to check if the player is currently attacking

    private readonly List<NPCHealth> enemiesInRange = new List<NPCHealth>();

    void Start()
    {
        lastAttackTime = -attackCooldown;
        isAttacking = false;
        Debug.Log("PlayerAttack initialized. Cooldown = " + attackCooldown + ", damage = " + attackDamage);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Attack input received at time " + Time.time);
            Attack();
        }
    }

    public void Attack()
    {
        Debug.Log("Attempting attack. Current time = " + Time.time + ", lastAttackTime = " + lastAttackTime);

        if (Time.time - lastAttackTime < attackCooldown)
        {
            Debug.Log("Attack blocked by cooldown. Time since last attack = " + (Time.time - lastAttackTime));
            return;
        }

        enemiesInRange.RemoveAll(enemy => enemy == null);
        Debug.Log("Enemies in range after cleanup: " + enemiesInRange.Count);

        if (enemiesInRange.Count == 0)
        {
            Debug.Log("Attack aborted: no enemies in range.");
            return;
        }

        isAttacking = true;
        Debug.Log("Starting attack on " + enemiesInRange.Count + " enemy(ies).");

        foreach (NPCHealth enemy in enemiesInRange)
        {
            if (enemy != null)
            {
                Debug.Log("Damaging enemy " + enemy.name + " (health before = " + enemy.currentHealth + ")");
                enemy.TakeDamage(attackDamage);
                Debug.Log("Enemy " + enemy.name + " took " + attackDamage + " damage and now has " + enemy.currentHealth + " health.");
            }
            else
            {
                Debug.Log("Skipped a null enemy reference in enemiesInRange.");
            }
        }

        lastAttackTime = Time.time;
        Debug.Log("Attack complete. Cooldown reset at " + lastAttackTime);
        isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger enter: " + collision.name + " with tag " + collision.tag);
        if (collision.CompareTag("Enemy"))
        {
            NPCHealth enemyHealth = collision.GetComponent<NPCHealth>();
            if (enemyHealth != null)
            {
                if (!enemiesInRange.Contains(enemyHealth))
                {
                    enemiesInRange.Add(enemyHealth);
                    Debug.Log("Enemy added to range: " + enemyHealth.name + ". Total enemies in range: " + enemiesInRange.Count);
                }
                else
                {
                    Debug.Log("Enemy already in range: " + enemyHealth.name);
                }
            }
            else
            {
                Debug.Log("Trigger entered by object tagged Enemy but NPCHealth component was missing: " + collision.name);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Trigger exit: " + collision.name + " with tag " + collision.tag);
        if (collision.CompareTag("Enemy"))
        {
            NPCHealth enemyHealth = collision.GetComponent<NPCHealth>();
            if (enemyHealth != null)
            {
                enemiesInRange.Remove(enemyHealth);
                Debug.Log("Enemy removed from range: " + enemyHealth.name + ". Total enemies in range: " + enemiesInRange.Count);
            }
            else
            {
                Debug.Log("Enemy left range but NPCHealth component was missing: " + collision.name);
            }
        }
    }
}

