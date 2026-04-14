using UnityEngine;
using System.Collections;
using System;

public class KnightAI : MonoBehaviour
{
    public float speed = 5f;
    public float sightRange;
    private Transform player; // Assign the player in inspector
    private Rigidbody2D rb;
    private bool playerRecentlyAttacked = false;
    public int damageAmount = 40;
    public float attackRange = 1.5f;
    public float hoverHeight = 10f;
    private bool isAttacking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) > sightRange)
        {
            transform.position = transform.position; // Stay idle if player is out of sight
        }
        else if (Vector2.Distance(transform.position, player.position) < sightRange)
        {
             float directionX = 0f;
            if (player.position.x > transform.position.x && Math.Abs(player.position.x - transform.position.x) > attackRange && playerRecentlyAttacked == false && isAttacking == false)
            {
                directionX = 1f; // Move Right
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, player.position.y + hoverHeight), speed * Time.deltaTime); // Stay above player
            }
            else if (player.position.x < transform.position.x && Math.Abs(player.position.x - transform.position.x) > attackRange && playerRecentlyAttacked == false && isAttacking == false)
            {
                directionX = -1f; // Move Left
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, player.position.y + hoverHeight), speed * Time.deltaTime);
            }

            if (Math.Abs(player.position.x - transform.position.x) < attackRange && playerRecentlyAttacked == false)
            {
                
                StartCoroutine(Attack());
            }

            if (player.position.x > transform.position.x && Vector2.Distance(transform.position, player.position) > attackRange && playerRecentlyAttacked == true)
            {
                directionX = -1f; // Move left
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, player.position.y + hoverHeight), speed * Time.deltaTime); // Stay above player
            }
            else if (player.position.x < transform.position.x && Vector2.Distance(transform.position, player.position) > attackRange && playerRecentlyAttacked == true)
            {
                directionX = 1f; // Move Right
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, player.position.y + hoverHeight), speed * Time.deltaTime);
            }
            if (isAttacking == true)
            {
                 transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }



            // Apply velocity, keeping existing vertical velocity (gravity)
            var lv = rb.linearVelocity;
            lv.x = directionX * speed;
            rb.linearVelocity = lv;

            // Optional: Flip the sprite based on movement direction
           if (directionX > 0) transform.localRotation = new Quaternion(0, 180, 0, 1);
           else if (directionX < 0) transform.localRotation = new Quaternion(0, 0, 0, 1);
        }
    }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
            if (isAttacking == true)
            {    
                // Implement damage logic here, e.g., reduce player's health

                    Health playerHealth = collision.GetComponent<Health>();
            if (playerHealth != null)
            {            playerHealth.TakeDamage(damageAmount); // Example damage amount
                Debug.Log("Player hit by enemy attack!");
                isAttacking = false; // Reset attack state after hitting the player
                playerRecentlyAttacked = true; // Set the flag to prevent immediate re-attacks
            }
            }
        }
        }

    private System.Collections.IEnumerator Attack()
    {
       
        speed = 10f; // Increase speed when attacking
        isAttacking = true;
        yield return new WaitForSeconds(3f);
        playerRecentlyAttacked = true;
        speed = 5f; // Reset speed after attack
        isAttacking = false;
        yield return new WaitForSeconds(4f); // Cooldown before next attack
        playerRecentlyAttacked = false;
      
}
}
