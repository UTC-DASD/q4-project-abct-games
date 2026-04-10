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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
       /* Vector2 direction = player.transform.position - transform.position;
float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
transform.rotation = Quaternion.Euler(0, 0, angle); */

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