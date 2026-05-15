using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class NewMonoBehaviourScript : MonoBehaviour
{
        public bool canInteract = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter2D(Collider2D other)
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
    }

    // Called when another collider exits the trigger zone
    void OnTriggerExit2D(Collider2D other)
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
        SceneManager.LoadScene(6);
    }

}
