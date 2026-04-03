using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage;
    public float attacktime;
    private HashSet<GameObject> damagedObjects = new HashSet<GameObject>(); // Set to track already damaged objects
    private Animator animator; // Reference to the Animator component
    public float attackCooldown = .7f;
    public float timeSinceLastAttack;
    public float currentTime;
    private float CooldownEndTime;

 private void Start()
    {  
        animator = GetComponent<Animator>(); // Get the Animator component attached to this GameObject
    }
 
}
