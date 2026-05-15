using UnityEngine;

public class Slash : MonoBehaviour
{
     private NPCHealth enemyHealth;
    public int damageAmount = 25;
    public float knockbackForce;
    private float trueKnockbackForce;
    private Rigidbody2D Rb;

   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        StartCoroutine(DestroyAfterTime());
    }

    void Update()
    {
        
        if (Rb.linearVelocity.x < 0f)
        {
            transform.transform.localRotation = new Quaternion(0, 180, 0, 1);
            trueKnockbackForce = -knockbackForce;
        }
        else if (Rb.linearVelocity.x > 0f)
        {
            transform.localRotation = new Quaternion(0, 0, 0, 1);
            trueKnockbackForce = knockbackForce;
        }
        
    }

    private System.Collections.IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(3f); // Adjust the time as needed
        Destroy(gameObject); // Destroy the projectile after the specified time
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("King") || collision.CompareTag("Queen"))
        {
            collision.GetComponent<NPCHealth>().stunned = true;
                NPCHealth enemyHealth = collision.GetComponent<NPCHealth>();
                Rigidbody2D enemyRigidbody = collision.GetComponent<Rigidbody2D>();
        if (enemyHealth != null)
        {   enemyHealth.TakeDamage(damageAmount);
            enemyRigidbody.AddForceX(trueKnockbackForce, ForceMode2D.Impulse);
            Destroy(gameObject);
        }
    }
}
}
