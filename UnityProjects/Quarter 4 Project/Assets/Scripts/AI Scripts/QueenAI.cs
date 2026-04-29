using UnityEngine;
using System.Collections;

public class QueenAI : MonoBehaviour
{
     private Transform player; // Assign the player in inspector
     private Transform king;
        public GameObject ground; // Assign the ground in inspector
    public float speed = 3f; 
    public float projectileSpeed = 10f;
    public GameObject projectilePrefab; // Assign the projectile prefab in inspector
    private Rigidbody2D rb;
    
    public float sightRange;
    public float shootForce = 1000f;
    public bool usedAbility1 = false;
    public bool usedAbility2 = false;
    public bool usedUltimate = false;
    public float collisionDamage1 = 20f;
    public float collisionDamage2 = 40f;
    public int projectileDamage1 = 10;
    public int projectileDamage2 = 20;
    public int projectileDamage3 = 15;
    public float ability1Cooldown = 5f;
    public float ability2Cooldown = 10f;
    public float ultimateCooldown = 30f;
    public float abilityRange = 30f;
    private float nextTimeToFire;
    private float fireRate = .1f;
    private bool isUsingAbility = false;
    public bool phase2 = false;
    private bool isUsingUltimate = false;
    private float startingY;
    private float verticalVelocity = 7f;
    private bool abilit1AOEActive = false;
    private float spreadAngle = 20f; // Total angle of the spread
    private int projectileCount = 3; // Number of projectiles in the spread
    private float phase2projectileCount = 7; // Number of projectiles in the spread for phase 2
    

     void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        king = GameObject.FindGameObjectWithTag("King").transform;
       
        rb = GetComponent<Rigidbody2D>();
        startingY = transform.position.y;
    }


    void Update()
    {

            float directionX = 0f;
        if (Vector2.Distance(transform.position, player.position) < sightRange)
        {
            ;
            if (!isUsingAbility || Vector2.Distance(transform.position, player.position) > abilityRange)
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
            if (!usedAbility1 && !isUsingAbility && player.position.x > transform.position.x - .2f && player.position.x < transform.position.x + .2f)
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
        
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Queen"), true); // Ignore collisions with player during ability
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Ground"), LayerMask.NameToLayer("Queen"), true); // Ignore collisions with ground during ability
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Default"), LayerMask.NameToLayer("Queen"), true);

        rb.AddForceY(30f, ForceMode2D.Impulse); // Example: Jump up as part of ability 1

        yield return new WaitForSeconds(0.5f); // Wait for the jump to reach its peak
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Queen"), false); // Re-enable collisions with player after jump
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Ground"), LayerMask.NameToLayer("Queen"), false); // Re-enable collisions with ground after jump
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Default"), LayerMask.NameToLayer("Queen"), false);
        abilit1AOEActive = true; // Activate AOE damage after the jump
        rb.AddForceY(-80f, ForceMode2D.Impulse); // Example: Jump up as part of ability 1

        yield return new WaitForSeconds(0.1f);
        
        isUsingAbility = false;
        yield return new WaitForSeconds(ability1Cooldown);
        usedAbility1 = false;
    }

    private System.Collections.IEnumerator UseAbility2()
    {
        
        isUsingAbility = true;
        usedAbility2 = true;
        if (!phase2)
        {
            // Implement ability 2 logic for phase 1 here (e.g., spawn projectiles, area attack, etc.)
            FireArcSpread();
            
        }
        else
        {
            // Implement ability 2 logic for phase 2 here (e.g., heal, shield, etc.)
            FireArcSpreadPhase2();
            
        }
        yield return new WaitForSeconds(0.1f); // Wait for the ability animation or effect to complete
       
        isUsingAbility = false;
        yield return new WaitForSeconds(ability2Cooldown);
        usedAbility2 = false;
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
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, startingY + 15f), verticalVelocity * Time.deltaTime);
            rb.gravityScale = 0f; // Disable gravity during ultimate attack

            if (Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + fireRate;
                
                shoot();
            }

            yield return new WaitForSeconds(10f);
            usedUltimate = true;
            isUsingUltimate = false;
            isUsingAbility = false;
            rb.gravityScale = 1f; // Re-enable gravity after ultimate attack
           

        yield return null; // Placeholder for any delay or animation during the ultimate attack
    }
    void shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Vector2 direction = (player.position - transform.position).normalized;
        projectile.GetComponent<Rigidbody2D>().linearVelocity = direction * projectileSpeed;
        projectilePrefab.GetComponent<Projectile>().damageAmount = projectileDamage1;
    }
    public void FireArcSpread()
{
    Vector2 directionToPlayer = (player.position - transform.position).normalized;
        float baseAngle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

        // 2. Calculate starting angle for the spread
        float startAngle = baseAngle - (spreadAngle / 2f);
        float angleStep = spreadAngle / (projectileCount - 1);

        for (int i = 0; i < projectileCount; i++)
        {
            // 3. Calculate angle for this specific bullet
            float currentAngle = startAngle + (angleStep * i);
            Quaternion rotation = Quaternion.Euler(0, 0, currentAngle);

            // 4. Instantiate and move
            GameObject proj = Instantiate(projectilePrefab, transform.position, rotation);
            proj.GetComponent<Rigidbody2D>().linearVelocity = rotation * Vector2.right * projectileSpeed;
            projectilePrefab.GetComponent<Projectile>().damageAmount = projectileDamage2;
        }
    }
     public void FireArcSpreadPhase2()
{
     Vector2 directionToPlayer = (player.position - transform.position).normalized;
        float baseAngle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

        // 2. Calculate starting angle for the spread
        float startAngle = baseAngle - (spreadAngle / 2f);
        float angleStep = spreadAngle / (phase2projectileCount - 1);

        for (int i = 0; i < phase2projectileCount; i++)
        {
            // 3. Calculate angle for this specific bullet
            float currentAngle = startAngle + (angleStep * i);
            Quaternion rotation = Quaternion.Euler(0, 0, currentAngle);

            // 4. Instantiate and move
            GameObject proj = Instantiate(projectilePrefab, transform.position, rotation);
            proj.GetComponent<Rigidbody2D>().linearVelocity = rotation * Vector2.right * projectileSpeed;
            projectilePrefab.GetComponent<Projectile>().damageAmount = projectileDamage2;
        }
}


}
