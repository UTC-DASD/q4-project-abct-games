using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;
using Unity.VisualScripting;
public class TeleportScript : MonoBehaviour
{
    public bool canInteract = false;
    public GameObject player;
    public Vector2 location = new Vector2(-186.6f, -5f);
    public GameObject Surprise;
    public GameObject SurpriseBackground;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Called when another collider enters the trigger zone
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
        player.transform.position = location;
        Surprise.SetActive(true);
        SurpriseBackground.SetActive(true);
    }

}

