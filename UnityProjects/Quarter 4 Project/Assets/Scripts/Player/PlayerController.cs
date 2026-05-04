using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Data.Common;
using System.Collections.Generic;
using Unity.Mathematics;

public class PlayerController : MonoBehaviour
{

    public GameObject levelstats;
    private PlayerController PC;
    public GameObject platformPrefab;
    public float canCreatePlatform = 0;
    public float platformDestroyDelay = 3f;
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
    public float dashDuration = 0.2f;
    private float mousePositionX;
    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    public bool isGrounded;
    private bool isDashing;
    
    private float dashTime;
    private int dashDirection;
    public bool canMove = true;


    private int Damage;
    public int BaseDamage;
    public float AttackCooldown;
    public float lastAttackTime = 0f;
    public bool IsAttacking = false;
    private bool canAttack = true;
    public readonly List<NPCHealth> enemiesInRange = new List<NPCHealth>();
    private bool AirAttack = false;
    private bool EnemyTookDamage = false;


    public bool RunActive;
    public bool JumpActive;
    public bool AttackActive;
    public bool DashActive;
    public bool CrouchActive;


        //Ability 3 bools
        public bool CanUseAbility3 = true;
    private bool A3Magician = false;
    private bool A3Empress = false;
    private bool A3Temperence = false;
    private bool A3Emperor = false;
    private bool A3Devil = false;
    private bool A3Lovers = false;
    private bool A3Star = false;
    private bool A3Moon = false;
    private bool A3Sun = false;
    private bool A3Judgement = false;
    private bool A3WoF = false;

        //Ability 4 bools
        public bool CanUseAbility4 = true;
    private bool A4Magician = false;
    private bool A4Empress = false;
    private bool A4Temperence = false;
    private bool A4Emperor = false;
    private bool A4Devil = false;
    private bool A4Lovers = false;
    private bool A4Star = false;
    private bool A4Moon = false;
    private bool A4Sun = false;
    private bool A4Judgement = false;
    private bool A4WoF = false;





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PC = GetComponent<PlayerController>();
        playerInput = GetComponent<PlayerInput>();
        startingRotationX = transform.eulerAngles.x;
        startingRotationY = transform.eulerAngles.y;
        timeSinceLastDash = dashCooldown;
        Damage = (int)math.round(BaseDamage * levelstats.GetComponent<LevelCharacteristics>().damageMod);
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
            if (timeSinceLastTapLeft <= doubleTapDelay && timeSinceLastDash >= dashCooldown)
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
            if (timeSinceLastTapRight <= doubleTapDelay && timeSinceLastDash >= dashCooldown)
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
        
        if (isGrounded == false && Input.GetKeyDown(KeyCode.S) && canCreatePlatform >= 1)
        {
            Instantiate(platformPrefab, transform.position + new Vector3(0, -1, 0), Quaternion.identity);
            canMove = false;
        }
        if (EnemyTookDamage == true)
        {
            StartCoroutine(EnemyReset());
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
            canCreatePlatform = 1;
        }
        if (collision.gameObject.CompareTag("Platform"))
        {
            StartCoroutine(WaitForSecondsRealTime(platformDestroyDelay));
            canCreatePlatform = 0;
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
        if (timeSinceLastDash < dashCooldown) return;
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
        canCreatePlatform = 1;
    }
    public void Attack(InputAction.CallbackContext context)
    {
        if(canAttack == true)
        {
        StartCoroutine(AttackRoutine());
        }
    }
    IEnumerator WaitForSecondsRealTime(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
    }

    private System.Collections.IEnumerator AttackRoutine()
    {
        if (isGrounded == true)
        {
        Debug.Log("Ground Attack Active");
        IsAttacking = true;
        canAttack = false;
        yield return new WaitForSeconds(.5f);
        IsAttacking = false;
        yield return new WaitForSeconds(AttackCooldown);
        canAttack = true;
        }
        if (isGrounded == false)
        {
            Debug.Log("Air Attack Active");
            AirAttack = true;
            rb.AddForceY(-30, ForceMode2D.Impulse);
            canAttack = false;
            yield return new WaitForSeconds(.5f);
            if (canAttack == true)
            {
                AirAttack = false;
                yield break;
            }
            yield return new WaitForSeconds(.3f);
            AirAttack = false;
            yield return new WaitForSeconds(AttackCooldown);
            canAttack = true;
        }
        
    }

    // For Basic Attacks
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("EnemyAttacked");
            NPCHealth enemyHealth = collision.GetComponent<NPCHealth>();
            if (enemyHealth != null)
            {
                if (IsAttacking == true && EnemyTookDamage == false)
                {
                    enemyHealth.TakeDamage(Damage);
                    EnemyTookDamage = true;
                }
                if (AirAttack == true && EnemyTookDamage == false)
                {
                    EnemyTookDamage = true;
                    rb.AddForceY(50, ForceMode2D.Impulse);
                    enemyHealth.TakeDamage(Damage);
                    canAttack = true;
                   
                }
            }
    }
    }

    private System.Collections.IEnumerator EnemyReset()
    {
        yield return new WaitForSeconds(.2f);
        EnemyTookDamage = false;
    }

    public void Ability1(InputAction.CallbackContext context)
    {
        
    }

    public void Ability2(InputAction.CallbackContext context)
    {
        
    }


    public void Ability3(InputAction.CallbackContext context)
    {
    if (CanUseAbility3 == true)
        {
            if (A3Devil == true)
            {
            StartCoroutine(Devil());
            }

            if (A3Emperor == true)
            {
            StartCoroutine(Emperor());
            }

            if (A3Empress == true)
            {
            StartCoroutine(Empress());
            }

            if (A3Judgement == true)
            {
            StartCoroutine(Judgement());
            }

            if (A3Lovers == true)
            {
            StartCoroutine(Lovers());
            }

            if (A3Magician == true)
            {
            StartCoroutine(Magician());
            }

            if (A3Moon == true)
            {
            StartCoroutine(Moon());
            }

            if (A3Star == true)
            {
            StartCoroutine(Star());
            }

            if (A3Sun == true)
            {
            StartCoroutine(Sun());
            }

            if (A3Temperence == true)
            {
            StartCoroutine(Temperance());
            }

            if (A3WoF == true)
            {
            StartCoroutine(WheelOfFortune());
            }
        }
    }
    

    public void Ability4(InputAction.CallbackContext context)
    {
        if (CanUseAbility4 == true)
        {
            if (A4Devil == true)
            {
            StartCoroutine(Devil());
            }

            if (A4Emperor == true)
            {
            StartCoroutine(Emperor());
            }

            if (A4Empress == true)
            {
            StartCoroutine(Empress());
            }

            if (A4Judgement == true)
            {
            StartCoroutine(Judgement());
            }

            if (A4Lovers == true)
            {
            StartCoroutine(Lovers());
            }

            if (A4Magician == true)
            {
            StartCoroutine(Magician());
            }

            if (A4Moon == true)
            {
            StartCoroutine(Moon());
            }

            if (A4Star == true)
            {
            StartCoroutine(Star());
            }

            if (A4Sun == true)
            {
            StartCoroutine(Sun());
            }

            if (A4Temperence == true)
            {
            StartCoroutine(Temperance());
            }

            if (A4WoF == true)
            {
            StartCoroutine(WheelOfFortune());
            }
        }
    }

    private System.Collections.IEnumerator Magician()
    {
        yield return new WaitForSeconds (15);
    }
    private System.Collections.IEnumerator Empress()
    {
        yield return new WaitForSeconds (15);
    }
    private System.Collections.IEnumerator Temperance()
    {
        yield return new WaitForSeconds (15);
    }
    private System.Collections.IEnumerator Emperor()
    {
        yield return new WaitForSeconds (15);
    }
    private System.Collections.IEnumerator Devil()
    {
        yield return new WaitForSeconds (15);
    }
    private System.Collections.IEnumerator Lovers()
    {
        yield return new WaitForSeconds (15);
    }
    private System.Collections.IEnumerator Star()
    {
        yield return new WaitForSeconds (15);
    }
    private System.Collections.IEnumerator Moon()
    {
        yield return new WaitForSeconds (15);
    }
    private System.Collections.IEnumerator  Sun()
    {
        yield return new WaitForSeconds (15);
    }
    private System.Collections.IEnumerator Judgement()
    {
        yield return new WaitForSeconds (15);
    }
    private System.Collections.IEnumerator WheelOfFortune()
    {
        yield return new WaitForSeconds (15);
    }

}
