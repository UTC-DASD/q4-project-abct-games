using UnityEngine;

public class BishopAI : MonoBehaviour
{
    private Transform player; // Assign the player in inspector
    private float attackRange = 5f; // Example attack range
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = player.transform.position - transform.position;
float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
transform.rotation = Quaternion.Euler(0, 0, angle);

    if (Vector2.Distance(transform.position, player.position) < attackRange)
    {
        // Implement attack logic here, e.g., shoot a projectile towards the player
        Debug.Log("Bishop is attacking the player!");
    }
}
}