using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
     private NPCHealth enemyHealth;
    public int damageAmount = 25;

    public float rotationSpeed = 100f; // Degrees per second
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DestroyAfterTime());
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
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
