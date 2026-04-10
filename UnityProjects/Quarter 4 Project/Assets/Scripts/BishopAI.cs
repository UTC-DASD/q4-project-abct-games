using System.Collections;
using UnityEngine;

public class BishopAI : MonoBehaviour
{
    private Transform player; // Assign the player in inspector
    private float attackRange = 10f; // Example attack range
    public float speed = 3f; 
    public float projectileSpeed = 10f;
    public float attackCooldown = 2f;
    public bool canAttack = true;
    public GameObject projectilePrefab; // Assign the projectile prefab in inspector
    private Rigidbody2D rb;

     void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
       /* Vector2 direction = player.transform.position - transform.position;
float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
transform.rotation = Quaternion.Euler(0, 0, angle); */

 float directionX = 0f;
       
        if (player.position.x - transform.position.x > attackRange)
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

            // Apply velocity, keeping existing vertical velocity (gravity)
            var lv = rb.linearVelocity;
            lv.x = directionX * speed;
            rb.linearVelocity = lv;

            // Optional: Flip the sprite
           if (directionX > 0) transform.localScale = new Vector3(-1, 1, 1);
           else if (directionX < 0) transform.localScale = new Vector3(1, 1, 1);




    if (Vector2.Distance(transform.position, player.position) < attackRange && player.transform.position.y > transform.position.y + 1f)
    {
        StartCoroutine(Attack());
    }
}

private IEnumerator Attack()
{
    if (canAttack == true)
    {
        canAttack = false;
        // Trigger attack animation or logic here
        Debug.Log("Bishop is attacking the player!");

        // Instantiate the projectile and set its direction towards the player
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Vector2 direction = (player.position - transform.position).normalized;
        projectile.GetComponent<Rigidbody2D>().linearVelocity = direction * projectileSpeed;

        yield return new WaitForSeconds(attackCooldown); // Wait for cooldown before allowing next attack
        canAttack = true;
    }
}
}