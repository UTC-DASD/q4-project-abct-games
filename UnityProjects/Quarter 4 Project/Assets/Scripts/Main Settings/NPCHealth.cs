using UnityEngine;

public class NPCHealth : MonoBehaviour
{
     [SerializeField] private int maxHealth = 100; // Total health
    public float currentHealth; // Current health value

    public CoinScript coin;
    public int coinGain;
    void Start()
    {
        currentHealth = maxHealth; // Set current health to max at the start
    }

    // Public method for other scripts to inflict damage
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount; // Reduce health by the damage amount
        Debug.Log(gameObject.name + " took " + damageAmount + " damage. Current Health: " + currentHealth);


        if (currentHealth <= 0)
        {
            Die(); // Handle death when health drops to or below zero
            coin.coinAmount += coinGain;
        }
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " has died.");
        // Add death animations, sound effects, game over logic here
        Destroy(gameObject); // Destroy the game object when health runs out
    }
}
