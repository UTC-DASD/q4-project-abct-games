using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

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
    private float timeSinceLastDash;
    public float dashCooldown = 2f;
    private float horizontal;
    private float startingRotationX;
    private float startingRotationY;
    public float playerPositionX;
    private float mousePositionX;
    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    public bool isGrounded;
    private bool isDashing;
    public float dashDuration = 0.2f;
    private float dashTime;
    private int dashDirection;
    public PlayerAbilities PlayerAbilities;
    public bool canMove = true;
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

        if (transform.position.x >= mousePositionX)
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
            if (timeSinceLastTapLeft <= doubleTapDelay && timeSinceLastDash > dashCooldown)
            {
                // Trigger Dash Left Logic
                dashDirection = -1;
                isDashing = true;
                dashTime = dashDuration;
                timeSinceLastDash = 0f; // Reset dash timer
                rb.AddForce(new Vector2(dashDirection * dashForce, 0), ForceMode2D.Impulse);
            }
            lastTapTimeLeft = Time.time;

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            float timeSinceLastTapRight = Time.time - lastTapTimeRight;
            if (timeSinceLastTapRight <= doubleTapDelay && timeSinceLastDash > dashCooldown)
            {
                // Trigger Dash Right Logic
                dashDirection = 1;
                isDashing = true;
                dashTime = dashDuration;
                timeSinceLastDash = 0f; // Reset dash timer
                rb.AddForce(new Vector2(dashDirection * dashForce, 0), ForceMode2D.Impulse);
            }
            lastTapTimeRight = Time.time;

        }
        UpdateDashTimer();
        if (isDashing)
        {
            dashTime -= Time.deltaTime;
            if (dashTime <= 0)
            {
                isDashing = false;
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isDashing)
        {
            rb.linearVelocity = new UnityEngine.Vector2(horizontal * speed, rb.linearVelocity.y);
        }
    }
    public void Move(InputAction.CallbackContext context)
    {
       if (canMove == true)
       {
        horizontal = context.ReadValue<UnityEngine.Vector2>().x;
       }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
        }
       
    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = false;
        }
         if (collision.gameObject.CompareTag("Ground"))
        {
            PlayerAbilities.canCreatePlatform = 1;
        }
        if (collision.gameObject.CompareTag("Platform"))
        {
            StartCoroutine(WaitForSecondsRealTime(PlayerAbilities.platformDestroyDelay));
            PlayerAbilities.canCreatePlatform = 0;
            canMove = true;
        }
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded == true && context.performed)
        {
            UnityEngine.Debug.Log("Jumped");
            rb.linearVelocity = new UnityEngine.Vector2(rb.linearVelocity.x, jumpPower);
        }
    }
    public void Dash(InputAction.CallbackContext context)
    {
        if (timeSinceLastDash <= dashCooldown) return;
        if (horizontal > 0)
        {
            dashDirection = 1;
        }
        else if (horizontal < 0)
        {
            dashDirection = -1;
        }
        else
        {
            dashDirection = (int)transform.localScale.x;
        }
        isDashing = true;
        dashTime = dashDuration;
        rb.AddForce(new Vector2(dashDirection * dashForce, 0), ForceMode2D.Impulse);
        Debug.Log("Dashed!");
        timeSinceLastDash = 0f;
        DashJump();
    }
    private void UpdateDashTimer()
    {
        timeSinceLastDash += Time.deltaTime;
    }
    public void DashJump()
    {
        isGrounded = true;
        StartCoroutine(WaitForSecondsRealTime(0.5f));
    }
    IEnumerator WaitForSecondsRealTime(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
    }
}
