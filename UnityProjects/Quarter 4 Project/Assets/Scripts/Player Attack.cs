using UnityEngine;
using System.Collections.Generic;

public class PlayerAttack : MonoBehaviour
{
    public int damage;
    public float attacktime;
    private HashSet<GameObject> damagedObjects = new HashSet<GameObject>(); // Set to track already damaged objects
    private Animator animator; // Reference to the Animator component
    public float attackCooldown = .7f;
    public float timeSinceLastAttack;
    public float currentTime;
    private float CooldownEndTime;
    private bool isAttacking = false;

 private void Start()
    {  
        animator = GetComponent<Animator>(); // Get the Animator component attached to this GameObject
    }

public void StartAttack()
{
     isAttacking = true;
        animator.SetTrigger("AttackTrigger"); // Trigger the attack animation
        damagedObjects.Clear(); // Clear the set of damaged objects at the start of an attack
        timeSinceLastAttack = currentTime; // Reset the timer
}
  private void OnTriggerEnter(Collider other) // Use OnTriggerEnter for trigger colliders
      {  // Try to get the Health component from the object we hit
        NPCHealth otherNPCHealth = other.GetComponent<NPCHealth>();
        

        // If the object has a Health component, call its TakeDamage method
        if (otherNPCHealth != null)
       {
         if (isAttacking && !damagedObjects.Contains(other.gameObject)) // Check if we are attacking and haven't already damaged this object
        {
            damagedObjects.Add(other.gameObject); // Add the object to the set of damaged objects

         otherNPCHealth.TakeDamage(damage); // Inflict damage using configured damage value
         Debug.Log("Damage dealt: " + damage);
        }
       }
}       
}
