using UnityEngine;
using System.Collections.Generic;

public class Damage : MonoBehaviour
{
    public float damageAmount = 10; // The amount of damage this object inflicts
    public bool isAttacking = false; // Flag to check if the weapon is currently attacking
    private HashSet<GameObject> damagedObjects = new HashSet<GameObject>(); // Set to track already damaged objects
    private Animator animator; // Reference to the Animator component

    public float attackCooldown = .7f;
    public float timeSinceLastAttack;
    public float currentTime;
    private float CooldownEndTime;
    public GameObject targetGameObject;
    public bool isPlayer = false; // Flag to check if this script is attached to the player character

    

    private void Start()
    {
        
        animator = GetComponent<Animator>(); // Get the Animator component attached to this GameObject
        if (animator == null)
        {
            Debug.LogWarning("No Animator component found on " + gameObject.name);
        }
    }

    private void Update()
    {
        currentTime = Time.time;
        
        if (Time.time >= CooldownEndTime)
        {
            EndAttack();
        }
    }
    
       
    public void StartAttack()
    {
        if (isPlayer)
        {
        isAttacking = true;
        animator.SetTrigger("AttackTrigger"); // Trigger the attack animation
        damagedObjects.Clear(); // Clear the set of damaged objects at the start of an attack
        timeSinceLastAttack = currentTime; // Reset the timer
        }
        else
        {
            isAttacking = true;
            animator.SetTrigger("NPCAttackTrigger"); // Trigger the attack animation
            damagedObjects.Clear(); // Clear the set of damaged objects at the start of an attack
            timeSinceLastAttack = currentTime; // Reset the timer
            CooldownEndTime = currentTime + attackCooldown;
        
        }

    }

    public void EndAttack()
    {
        isAttacking = false;
    }
    private void OnTriggerEnter(Collider other) // Use OnTriggerEnter for trigger colliders
    {
        
            
    

        // Try to get the Health component from the object we hit
        Health otherHealth = other.GetComponent<Health>();
        NPCHealth otherNPCHealth = other.GetComponent<NPCHealth>();
        

        // If the object has a Health component, call its TakeDamage method
        if (otherHealth != null)
       {
         if (isAttacking && !damagedObjects.Contains(other.gameObject)) // Check if we are attacking and haven't already damaged this object
        {
            damagedObjects.Add(other.gameObject); // Add the object to the set of damaged objects

         otherHealth.TakeDamage(Mathf.RoundToInt(damageAmount)); // Inflict damage considering pierce and damage reduction
         Debug.Log("Damage dealt: " + Mathf.RoundToInt(damageAmount));
        }
       }
        else if (otherNPCHealth != null)
        {
            if (isAttacking && !damagedObjects.Contains(other.gameObject)) // Check if we are attacking and haven't already damaged this object
            {
                damagedObjects.Add(other.gameObject); // Add the object to the set of damaged objects

                otherNPCHealth.TakeDamage(Mathf.RoundToInt(damageAmount)); // Inflict damage considering pierce and damage reduction
                Debug.Log("Damage dealt: " + Mathf.RoundToInt(damageAmount));
            }
        }
    }
}