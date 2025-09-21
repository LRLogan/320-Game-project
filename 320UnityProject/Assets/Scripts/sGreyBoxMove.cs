using UnityEngine;

public class IsoPlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 5f;
    public float runSpeed = 12f;

    [Header("Jumping")]
    public float jumpForce = 7f; 
    public float stage1Multiplier = 1.2f;
    public float stage2Multiplier = 1.6f;
    public float stage3Multiplier = 2f;
    public float holdToStartCharge = 1f; 
    public float fullChargeTime = 3f;    
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.2f;


    private Rigidbody rb;
    private bool isGrounded;

    private float jumpHoldTime = 0f;
    private bool isCharging = false;
    private bool jumpButtonHeld = false;

    private Renderer rend;
    private Vector3 originalCameraPos;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        //Detect jump input
        jumpButtonHeld = Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.JoystickButton0);

        if (jumpButtonHeld && isGrounded)
        {
            jumpHoldTime += Time.deltaTime;

            if (jumpHoldTime >= holdToStartCharge)
                isCharging = true;

            //Color feedback for charging stages
            if (isCharging)
            {
                float chargeProgress = Mathf.Clamp((jumpHoldTime - holdToStartCharge) / fullChargeTime, 0f, 1f);
                if (chargeProgress < 0.33f)
                    rend.material.color = Color.yellow;
                else if (chargeProgress < 0.66f)
                    rend.material.color = new Color(1f, 0.5f, 0f); 
                else
                    rend.material.color = Color.red;
            }
        }

        //Jump release
        if ((Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.JoystickButton0)) && isGrounded)
        {
            float finalJumpForce = jumpForce;

            if (isCharging)
            {
                //Charge jump calculation
                float chargeProgress = Mathf.Clamp((jumpHoldTime - holdToStartCharge) / fullChargeTime, 0f, 1f);

                if (chargeProgress < 0.33f)
                    finalJumpForce *= stage1Multiplier;
                else if (chargeProgress < 0.66f)
                    finalJumpForce *= stage2Multiplier;
                else
                    finalJumpForce *= stage3Multiplier;
            }

            rb.AddForce(Vector3.up * finalJumpForce, ForceMode.Impulse);

            //Reset
            jumpHoldTime = 0f;
            isCharging = false;
            rend.material.color = Color.green;
        }

    }

    void FixedUpdate()
    {
        
        if (!isCharging)
            HandleMovement();

        CheckGrounded();
    }

    void HandleMovement()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        bool isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.JoystickButton2);
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        Vector3 isoRight = new Vector3(1, 0, -1).normalized;
        Vector3 isoUp = new Vector3(1, 0, 1).normalized;
        Vector3 move = (isoRight * h + isoUp * v).normalized;

        if (move != Vector3.zero)
            rb.MovePosition(rb.position + move * currentSpeed * Time.fixedDeltaTime);
    }

    void CheckGrounded()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);

        if (isGrounded && !jumpButtonHeld)
        {
            jumpHoldTime = 0f;
            isCharging = false;
            rend.material.color = Color.green;
        }
    }
}
