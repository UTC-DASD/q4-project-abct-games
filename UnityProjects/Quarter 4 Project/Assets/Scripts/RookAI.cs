using UnityEngine;

public class RookAI : MonoBehaviour
{
        public Transform player; // Assign the player in inspector
    public float speed = 80f;
    private Rigidbody2D rb;
     private Health playerHealth;
    public int damageAmount = 80;
    public bool canMove = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (player != null)
        {
            // Only compare X position to move toward player
            float directionX = 0f;
            if (player.position.x > transform.position.x && player.position.y < transform.position.y && canMove)
            {
                rb.AddForce(new Vector2(1f, 0) * speed, ForceMode2D.Impulse); // Move Right
                directionX = 1f;
            }
            else if (player.position.x < transform.position.x && player.position.y < transform.position.y && canMove)
            {
                rb.AddForce(new Vector2(-1f, 0) * speed, ForceMode2D.Impulse); // Move Left
                directionX = -1f;
            }

            // Apply velocity, keeping existing vertical velocity (gravity)
            var lv = new Vector2(directionX * speed, rb.linearVelocity.y);
            rb.linearVelocity = lv;

            // Optional: Flip the sprite
           if (directionX > 0) transform.localScale = new Vector3(-1, 1, 1);
           else if (directionX < 0) transform.localScale = new Vector3(1, 1, 1);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Implement damage logic here, e.g., reduce player's health
                Health playerHealth = collision.GetComponent<Health>();
        if (playerHealth != null)
        {            playerHealth.TakeDamage(damageAmount); // Example damage amount
            Debug.Log("Player hit by enemy attack!");
        }
    }
}

  
}
