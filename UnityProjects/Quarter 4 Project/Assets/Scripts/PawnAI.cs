using UnityEngine;
using UnityEngine.AI;

public class PawnAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    // States
    public float sightRange, attackRange;
    bool playerInSightRange, playerInAttackRange;

    // Attacking
    public float timeBetweenAttacks;
    public float timeBetweenHeavyAttacks;
    bool alreadyAttacked;
    public GameObject weaponObject; // Reference to the weapon GameObject
    public float rotationx;
    public float startRotationX;
    public float startRotationY;
    public float rotationY;
    public float rotationSpeed = 5f;

  private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        startRotationX = transform.rotation.eulerAngles.x;
        startRotationY = transform.rotation.eulerAngles.y;
        playerInSightRange = Physics2D.OverlapCircle(transform.position, sightRange);
        playerInAttackRange = Physics2D.OverlapCircle(transform.position, attackRange);
    }

    private void Update()
    {
       
        
        rotationx = transform.rotation.eulerAngles.x;
        transform.rotation = Quaternion.Euler(startRotationX, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        rotationY = transform.rotation.eulerAngles.y;

        if (Vector3.Distance(transform.position, player.position) <= sightRange && Vector3.Distance(transform.position, player.position) > attackRange)
        {
            //transform.LookAt(player);
               // 1. Calculate the direction vector
    Vector3 direction = player.position - transform.position;
    // 2. Create the target rotation
    Quaternion targetRotation = Quaternion.LookRotation(direction);
    // 3. Gradually rotate towards it
    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);


            ChasePlayer();
        }  
        else
        {
           agent.SetDestination(transform.position); // Player is not in sight or attack range, do nothing or patrol
        }

        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
         AttackPlayer();
        }
    }

    private void ChasePlayer()
    {
//        agent.SetDestination(player.position);

    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position); // Stop moving
        Vector3 direction = player.position - transform.position;
    // 2. Create the target rotation
    Quaternion targetRotation = Quaternion.LookRotation(direction);
    // 3. Gradually rotate towards it
    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        

        if (!alreadyAttacked)
        {
           Debug.Log ("Attacking the player!");
           weaponObject.GetComponent<Damage>().StartAttack(); // Call the StartAttack method from the Weapons script
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

}
