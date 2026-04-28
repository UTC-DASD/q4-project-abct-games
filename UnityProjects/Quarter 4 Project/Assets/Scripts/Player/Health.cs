using UnityEngine;
using UnityEngine.InputSystem;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100; // Total health
    public float currentHealth; // Current health value
    public bool canTakeDamage = true;
    public Animator animator;
    public bool CanBlock = true;
    public float BlockCooldown;
    

    void Start()
    {
        currentHealth = maxHealth; // Set current health to max at the start
    }

    // Public method for other scripts to inflict damage
    public void TakeDamage(int damageAmount)
    {
        if(canTakeDamage == true)
        {
        currentHealth -= damageAmount; // Reduce health by the damage amount
        Debug.Log(gameObject.name + " took " + damageAmount + " damage. Current Health: " + currentHealth);
        }

        if (currentHealth <= 0)
        {
            Die(); // Handle death when health drops to or below zero
        }
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " has died.");
        // Add death animations, sound effects, game over logic here
        Destroy(gameObject); // Destroy the game object when health runs out
        if (gameObject.CompareTag("Player"))
        {
           #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in the editor
              #else
            Application.Quit(); // Quit the application in a build
              #endif
        }
    }

    // Optional: Add a heal method
    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void Block(InputAction.CallbackContext context)
    {
       StartCoroutine(Block());
    }

    private System.Collections.IEnumerator Block()
    {
        if(CanBlock == true)
        {
        canTakeDamage = false;
        CanBlock = false;
        }
        yield return new WaitForSeconds(1);
        canTakeDamage = true;
        yield return new WaitForSeconds(BlockCooldown);
        CanBlock = true;  
        Debug.Log("Blocking");
    }
}
