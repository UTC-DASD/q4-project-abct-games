using UnityEngine;
using UnityEngine.InputSystem;
public class HealingTable : MonoBehaviour{

    public GameObject player;
    public GameObject surprise;
    // Flag to track if the player is in the trigger zone
    private bool playerInRange = false;

    private bool canInteract = false;
    private Health healthScript;
    void Start()
    {
        
    }

    // Called when another collider enters the trigger zone
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Check if the entering object is the player
        {
            canInteract = true;
            Debug.Log("Press E to interact"); // Or show UI prompt
        }
    }

    // Called once per frame while the player is in the trigger zone
    void Update()
    {
        if (canInteract && Keyboard.current.eKey.IsPressed()) // Check for interaction input (e.g., 'E' key)
        {
            Interact();
        }
        if (canInteract && Keyboard.current.cKey.IsPressed()) // Check for interaction input (e.g., 'E' key)
        {
            Secret();
        }
    }

    // Called when another collider exits the trigger zone
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
            Debug.Log("Interaction disabled"); // Or hide UI prompt
        }
    }
    public void Interact()
    {
        Debug.Log("Interaction occurred!");
        // Add your custom interaction code here (e.g., open a door, pick up an item, etc.)
        // If using UnityEvent:
        healthScript.Heal(30);
    }

    public void Secret()
    {
        surprise.SetActive(true);
    }
}