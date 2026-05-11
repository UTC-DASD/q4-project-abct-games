using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
     private NPCHealth enemyHealth;
    public int damageAmount = 25;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DestroyAfterTime());
    }

    private System.Collections.IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(3f); // Adjust the time as needed
        Destroy(gameObject); // Destroy the projectile after the specified time
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Flyer"))
        {
                NPCHealth enemyHealth = collision.GetComponent<NPCHealth>();
        if (enemyHealth != null)
        {            enemyHealth.TakeDamage(damageAmount);
            Destroy(gameObject);
        }
    }
}
}
