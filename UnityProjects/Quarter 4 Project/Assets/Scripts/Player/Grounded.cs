using UnityEngine;

public class Grounded : MonoBehaviour
{
    private GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Debug.Log("Found Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            player.GetComponent<PlayerController>().isGrounded = true;
        }
       
    } 
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            player.GetComponent<PlayerController>().isGrounded = false;
        }
        
        if (collision.gameObject.CompareTag("Ground"))
        {
            player.GetComponent<PlayerController>().canCreatePlatform = 1;
        }
}
}
