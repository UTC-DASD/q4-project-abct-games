using UnityEngine;
using System.Collections;

public class QueenAI : MonoBehaviour
{
     private Transform player; // Assign the player in inspector
    public float speed = 3f; 
    public float projectileSpeed = 10f;
    public GameObject projectilePrefab; // Assign the projectile prefab in inspector
    private Rigidbody2D rb;
    public float sightRange;
    public bool usedAbility1 = false;
    public bool usedAbility2 = false;
    public bool usedUltimate = false;
    public float collisionDamage1 = 20f;
    public float collisionDamage2 = 40f;
    public float projectileDamage1 = 10f;
    public float projectileDamage2 = 20f;
    public float ability1Cooldown = 5f;
    public float ability2Cooldown = 10f;
    private bool isUsingAbility = false;
    private bool phase2 = false;

     void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) < sightRange)
        {
            if (!usedAbility1 && !isUsingAbility)
            {
                StartCoroutine(UseAbility1());
            }
            else if (!usedAbility2 && !isUsingAbility)
            {
                StartCoroutine(UseAbility2());
            }
            else if (phase2 && !isUsingAbility)
            {
                StartCoroutine(ProtectKing());
            }
            else if (!phase2 && !isUsingAbility)
            {
                StartCoroutine(AttackPlayer());
            }
        }
    }

    private System.Collections.IEnumerator UseAbility1()
    {
        isUsingAbility = true;
        usedAbility1 = true;
        // Implement ability 1 logic here (e.g., spawn projectiles, increase speed, etc.)





        yield return new WaitForSeconds(ability1Cooldown);
        isUsingAbility = false;
    }

    private System.Collections.IEnumerator UseAbility2()
    {
        isUsingAbility = true;
        usedAbility2 = true;
        // Implement ability 2 logic here (e.g., spawn projectiles, increase speed, etc.)

        yield return new WaitForSeconds(ability2Cooldown);
        isUsingAbility = false;
    }

    private System.Collections.IEnumerator PhaseTransition()
    {
        phase2 = true;
        // Implement phase transition logic here (e.g., change appearance, increase stats, etc.)

        yield return null; // Placeholder for any delay or animation during the transition
    }

    private System.Collections.IEnumerator ProtectKing()
    {
        // Implement logic to protect the king here (e.g., move towards the king, block attacks, etc.)

        yield return null; // Placeholder for any delay or animation during the protection
        
    }

    private System.Collections.IEnumerator AttackPlayer()
    {
        // Implement attack logic here (e.g., spawn projectiles, move towards player, etc.)

        yield return null; // Placeholder for any delay between attacks
    }

    private System.Collections.IEnumerator UltimateAttack()
    {
        usedUltimate = true;
        // Implement ultimate attack logic here (e.g., spawn powerful projectiles, area of effect attack, etc.)

        yield return null; // Placeholder for any delay or animation during the ultimate attack
    }



}
