using UnityEngine;

public class Slash : MonoBehaviour
{
     private NPCHealth enemyHealth;
    public int damageAmount = 25;
    public float knockbackForce;

   
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
            Rigidbody2D enemyRigidbody = collision.GetComponent<Rigidbody2D>();
            if (enemyRigidbody != null)
            {
                Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
                enemyRigidbody.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            }
            Destroy(gameObject);
        }
    }
}
}
