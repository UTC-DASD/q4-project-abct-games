using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

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
    public int collisionDamage1 = 20;
    public int collisionDamage2 = 40;
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
    private bool attackPlayer = false;
    private bool recentlyAttacked = false;
    private bool playerhit = false;
    

     void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        king = GameObject.FindGameObjectWithTag("King").transform;
       
        rb = GetComponent<Rigidbody2D>();
        startingY = transform.position.y;
    }


    void Update()
    {
        if (GetComponent<NPCHealth>().stunned == false)
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
            else if (Vector2.Distance(king.position, player.position) < 2)
            {
                StartCoroutine(ProtectKing());
                
            }
            else if (!isUsingAbility && player.position.y > transform.position.y - 1f && player.position.y < transform.position.y + 1f && recentlyAttacked == false)
            {
                StartCoroutine(AttackPlayer());
                
            }
            if (!usedUltimate && !isUsingAbility && phase2 == true || isUsingUltimate == true && phase2 == true)
            {
                StartCoroutine(UltimateAttack());
                
            }
            if (king.GetComponent<NPCHealth>().currentHealth < (king.GetComponent<NPCHealth>().maxHealth / 2))
            {
                phase2 = true;
            }
            if (playerhit == true)
            {
                StartCoroutine(AttackReset());
            }
            
          if (attackPlayer == false)
            {
            var lv = rb.linearVelocity;
            lv.x = directionX * speed;
            rb.linearVelocity = lv;
            }
       
        }
        }
        
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (abilit1AOEActive && collision.gameObject.CompareTag("Ground"))
        {
            foreach (var hit in Physics2D.CircleCastAll(transform.position, 5f, Vector2.zero))
            {
                if (hit.collider != null && hit.collider.CompareTag("Player"))
                {
                    hit.collider.gameObject.GetComponent<Health>().TakeDamage(collisionDamage1);
                }
            }
        }
        if (collision.gameObject.CompareTag("Player") && attackPlayer == true && playerhit == false)
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(collisionDamage2);
            playerhit = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && abilit1AOEActive == true)
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(collisionDamage1);
        }
    }

    private System.Collections.IEnumerator AttackReset()
    {
        
            yield return new WaitForSeconds(1f); // Wait for 1 second before allowing movement again
            playerhit = false;
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
        abilit1AOEActive = false; // Deactivate AOE damage after a short duration
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

    private System.Collections.IEnumerator ProtectKing()
    {
        yield return new WaitForSeconds(1);
        transform.position = king.transform.position;
        
    }

    private System.Collections.IEnumerator AttackPlayer()
    {
        attackPlayer = true;
        recentlyAttacked = true;
        if (player.position.x > transform.position.x)
        {
            rb.AddForceX(10f, ForceMode2D.Impulse); // Example: Dash right towards player
        }
        else
        {
            rb.AddForceX(-10f, ForceMode2D.Impulse); // Example: Dash left towards player
        }
        yield return new WaitForSeconds(0.5f); // Wait for the dash to complete
        attackPlayer = false;
        yield return new WaitForSeconds(2f); // Cooldown before the next attack
        recentlyAttacked = false;

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
