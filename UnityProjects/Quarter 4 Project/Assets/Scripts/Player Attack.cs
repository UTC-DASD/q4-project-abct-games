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
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    public void Attack()
    {
        if (Time.time - lastAttackTime < attackCooldown)
        {
            return;
        }

        enemiesInRange.RemoveAll(enemy => enemy == null);

        if (enemiesInRange.Count == 0)
        {
            return;
        }

        isAttacking = true;

        foreach (NPCHealth enemy in enemiesInRange)
        {
            if (enemy != null)
            {
                enemy.TakeDamage(attackDamage);
            }
        }

        lastAttackTime = Time.time;
        isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            NPCHealth enemyHealth = collision.GetComponent<NPCHealth>();
            if (enemyHealth != null && !enemiesInRange.Contains(enemyHealth))
            {
                enemiesInRange.Add(enemyHealth);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            NPCHealth enemyHealth = collision.GetComponent<NPCHealth>();
            if (enemyHealth != null)
            {
                enemiesInRange.Remove(enemyHealth);
            }
        }
    }
}

