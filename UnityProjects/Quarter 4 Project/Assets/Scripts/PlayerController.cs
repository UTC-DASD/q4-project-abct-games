using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    float lastTapTimeLeft;
    float lastTapTimeRight; 
    public float doubleTapDelay = 0.2f;
    public Rigidbody2D rb;
    private PlayerInput playerInput;
    public float speed;
    public float jumpPower;
    private float horizontal;
    private float startingRotationX;
    private float startingRotationY;
    public float playerPositionX;
    private float mousePositionX;
    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    public bool isGrounded;
    public float velocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        startingRotationX = transform.eulerAngles.x;
        startingRotationY = transform.eulerAngles.y;
    }
    void Update()
    {
        //Vector2 currentvelocity2D = rb.linearVelocity;
        int Velocity = (int)GetComponent<Rigidbody>().linearVelocity.magnitude;
        // Reset rotation to prevent tilting
        transform.rotation = Quaternion.Euler(startingRotationX, startingRotationY, 0);
        playerPositionX = transform.position.x;
        if (Input.GetKeyDown(KeyCode.A))
        {
            float timeSinceLastTapLeft = Time.time - lastTapTimeLeft;
            if (timeSinceLastTapLeft <= doubleTapDelay)
            {
                // Trigger Dash Left Logic
            }
            lastTapTimeLeft = Time.time;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            float timeSinceLastTapRight = Time.time - lastTapTimeRight;
            if (timeSinceLastTapRight <= doubleTapDelay)
            {
                // Trigger Dash Right Logic
            }
            lastTapTimeRight = Time.time;
        }
        if (Velocity < 0)
        {
            
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocity = new UnityEngine.Vector2(horizontal * speed, rb.linearVelocity.y);
        
    }
    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<UnityEngine.Vector2>().x;
    }
    private bool IsGrounded()

        {
    float extraHeightText = 0.1f;
    RaycastHit2D raycastHit = Physics2D.Raycast(GetComponent<Collider2D>().bounds.center, Vector2.down, GetComponent<Collider2D>().bounds.extents.y + extraHeightText);
    return raycastHit.collider != null;
}

    public void Jump(InputAction.CallbackContext context)
    {
        if (IsGrounded() && context.performed)
        {
            UnityEngine.Debug.Log("Jumped");
            rb.linearVelocity = new UnityEngine.Vector2(rb.linearVelocity.x, jumpPower);
        }
}
    public void Dash(InputAction.CallbackContext context)
    {
        
    }

    public void Attack(InputAction.CallbackContext context)
    {
        
    }

    public void Crouch(InputAction.CallbackContext context)
    {
        
    }
}