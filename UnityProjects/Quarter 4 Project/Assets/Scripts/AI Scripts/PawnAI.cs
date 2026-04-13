using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Collections;

public class PawnAI : MonoBehaviour
{
     public Transform player; // Assign the player in inspector
    public float speed = 3f;
    private Rigidbody2D rb;
    private float initialYScale;
    private float initialZScale;
    public float sightRange;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialYScale = transform.localScale.y;
        initialZScale = transform.localScale.z;
    }

    void Update()
    {
         if (Vector2.Distance(transform.position, player.position) < sightRange)
        {
        if (player != null)
        {
            // Only compare X position to move toward player
            float directionX = 0f;
            if (player.position.x > transform.position.x)
            {
                directionX = 1f; // Move Right
            }
            else if (player.position.x < transform.position.x)
            {
                directionX = -1f; // Move Left
            }

            // Apply velocity, keeping existing vertical velocity (gravity)
            var lv = rb.linearVelocity;
            lv.x = directionX * speed;
            rb.linearVelocity = lv;

            // Optional: Flip the sprite
           if (directionX > 0) transform.localScale = new Vector3(-1, initialYScale, initialZScale);
           else if (directionX < 0) transform.localScale = new Vector3(1, initialYScale, initialZScale);
        }
        }
    }
   /*public GameObject player;
   public float Speed;

   private float Distance;

   void Awake()
    {

    }

    void Update()
    {
        Distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;

        if (Distance > 1)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, Speed * Time.deltaTime);
        }
    }*/
}
