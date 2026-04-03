using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    float lastTapTimeLeft;
    float lastTapTimeRight; 
    public float doubleTapDelay = .2f;
    public Rigidbody2D rb;
    private PlayerInput playerInput;
    public float speed;
    public float jumpPower;
    public float dashForce = 20f;
    private float horizontal;
    private float startingRotationX;
    private float startingRotationY;
    public float playerPositionX;
    private float mousePositionX;
    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    public bool isGrounded;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        startingRotationX = transform.eulerAngles.x;
        startingRotationY = transform.eulerAngles.y;
    }
    void Update()
    {
        // Reset rotation to prevent tilting
        transform.rotation = Quaternion.Euler(startingRotationX, startingRotationY, 0);
        playerPositionX = transform.position.x;

        // Input System mouse position (safe fallback for legacy input)
        Vector2 screenMousePos;
        if (Mouse.current != null)
        {
            screenMousePos = Mouse.current.position.ReadValue();
        }
        else
        {
            screenMousePos = Input.mousePosition;
        }

        if (Camera.main != null)
        {
            mousePositionX = Camera.main.ScreenToWorldPoint(screenMousePos).x;
        }
        else
        {
            // fallback: assume center screen if no main camera
            mousePositionX = 0f;
            Debug.LogWarning("PlayerController: Camera.main is null. Assign a MainCamera tag to your camera.");
        }

        if (transform.position.x > mousePositionX)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (transform.position.x < mousePositionX)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }






        if (Input.GetKeyDown(KeyCode.A))
        {
            float timeSinceLastTapLeft = Time.time - lastTapTimeLeft;
            if (timeSinceLastTapLeft <= doubleTapDelay)
            {
                // Trigger Dash Left Logic
                Dash(Vector2.left);
            }
            lastTapTimeLeft = Time.time;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            float timeSinceLastTapRight = Time.time - lastTapTimeRight;
            if (timeSinceLastTapRight <= doubleTapDelay)
            {
                // Trigger Dash Right Logic
                Dash(Vector2.right);
            }
            lastTapTimeRight = Time.time;
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
    private void Dash(Vector2 direction) {
        rb.linearVelocity = direction * dashForce;
        Debug.Log("Dashed!");

}
}