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
    public float ultimateCooldown = 30f;
    private float nextTimeToFire;
    private float fireRate = .1f;
    private bool isUsingAbility = false;
    public bool phase2 = false;
    private bool isUsingUltimate = false;
    

     void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {

            float directionX = 0f;
        if (Vector2.Distance(transform.position, player.position) < sightRange)
        {
            if (!isUsingAbility)
            {
                if (player.position.x > transform.position.x)
            {
                directionX = 1f; // Move Right
            }
            else if (player.position.x < transform.position.x)
            {
                directionX = -1f; // Move Left
            }
            }
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
            if (!usedUltimate && !isUsingAbility && phase2 == true || isUsingUltimate == true && phase2 == true)
            {
                Debug.Log("Using Ultimate");
                StartCoroutine(UltimateAttack());
            }
           // Apply velocity, keeping existing vertical velocity (gravity)
            var lv = rb.linearVelocity;
            lv.x = directionX * speed;
            rb.linearVelocity = lv;
       
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
            isUsingAbility = true;
            isUsingUltimate = true;

            if (Time.time >= nextTimeToFire)
            {
            nextTimeToFire = Time.time + fireRate;
            shoot();
            }
      
       
        yield return new WaitForSeconds(10f);
        usedUltimate = true;
        isUsingUltimate = false;
        isUsingAbility = false;

        yield return null; // Placeholder for any delay or animation during the ultimate attack
    }
    void shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Vector2 direction = (player.position - transform.position).normalized;
        projectile.GetComponent<Rigidbody2D>().linearVelocity = direction * projectileSpeed;
    }



}
