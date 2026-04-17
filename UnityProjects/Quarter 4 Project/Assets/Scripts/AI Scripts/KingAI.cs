using UnityEngine;

public class KingAI : MonoBehaviour
{
    private Transform player;
    private Transform queen;
    public float speed = 5;
    public bool panicked = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        queen = GameObject.FindGameObjectWithTag("Queen").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.x > queen.position.x && panicked == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2 (queen.position.x - 1, transform.position.y), speed * Time.deltaTime);

        }
        if (player.position.x < queen.position.x && panicked == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2 (queen.position.x + 1, transform.position.y), speed * Time.deltaTime);

        }
        if (panicked == true)
        {
            

        }
    }
}
