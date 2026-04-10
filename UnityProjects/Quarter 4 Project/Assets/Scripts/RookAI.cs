using UnityEngine;

public class RookAI : MonoBehaviour
{
        public Transform player; // Assign the player in inspector
    public float speed = 80f;
    private Rigidbody2D rb;
     private Health playerHealth;
    public int damageAmount = 80;
    public bool canMove = false;

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
            if (player.position.x > transform.position.x && player.position.y < transform.position.y && canMove == true) 
            {
                StartCoroutine(MoveRight());
            }
            else if (player.position.x < transform.position.x && player.position.y < transform.position.y && canMove == true)
            {   
               StartCoroutine(MoveLeft());
            }

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
    private System.Collections.IEnumerator MovementReset()
    {
        if (canMove == false) // Allow movement again after a short delay
        {
            yield return new WaitForSeconds(3f); // Wait for 3 seconds
            canMove = true;
        }
    }
    private System.Collections.IEnumerator MoveRight()
    {
        if (canMove == true)
        {
            canMove = false; // Prevent further movement until next update
            yield return new WaitForSeconds(1f);
            rb.AddForce(new Vector2(1f, 0) * speed, ForceMode2D.Impulse); // Move Right
            StartCoroutine(MovementReset());
        }
    }
    private System.Collections.IEnumerator MoveLeft()
    {
        if (canMove == true)
        {
            canMove = false; // Prevent further movement until next update
            yield return new WaitForSeconds(1f);
            rb.AddForce(new Vector2(-1f, 0) * speed, ForceMode2D.Impulse); // Move Left
            StartCoroutine(MovementReset());
        }
    }

  
}
