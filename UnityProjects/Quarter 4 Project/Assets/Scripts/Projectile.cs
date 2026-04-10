using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Health playerHealth;
    public int damageAmount = 25;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        WaitForSecondsRealtime wait = new WaitForSecondsRealtime(3f); // Adjust the time as needed
        Destroy(gameObject); // Destroy the projectile after the specified time
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
            Destroy(gameObject); // Destroy the projectile after hitting the player
        }
    }
}
}
