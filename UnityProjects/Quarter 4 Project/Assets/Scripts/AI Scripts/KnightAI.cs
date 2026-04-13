using UnityEngine;

public class KnightAI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed = 3f;
    public float sightRange;
     public Transform player; // Assign the player in inspector
    private Rigidbody2D rb;
    private bool playerRecentlyAttacked = false;
    public int damageAmount = 40;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            // Move towards the player
            Vector2 direction = (player.position - transform.position).normalized;
            Vector2 lv = rb.linearVelocity;
            lv.x = direction.x * speed;
            rb.linearVelocity = lv;

            // Optional: Flip the sprite based on movement direction
           if (direction.x > 0) transform.localRotation = new Quaternion(0, 180, 0, 1);
           else if (direction.x < 0) transform.localRotation = new Quaternion(0, 0, 0, 1);
        }







}
