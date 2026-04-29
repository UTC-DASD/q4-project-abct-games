using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Health playerHealth;
    public int damageAmount = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
