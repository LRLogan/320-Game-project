using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class Player : MonoBehaviour
{
    // Movement 
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 12f;
    private float moveAngleOffset = -45f;

    [Header("Jumping")]
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float stage1Multiplier = 1.2f;
    [SerializeField] private float stage2Multiplier = 1.6f;
    [SerializeField] private float stage3Multiplier = 2f;
    [SerializeField] private float holdToStartCharge = 1f;
    [SerializeField] private float fullChargeTime = 3f;
    [SerializeField] private float gravity = -9.8f;
    private float jumpHoldTime = 0f;
    float chargeProgress = 0f;
    private bool isCharging = false;

    // Player object
    [SerializeField] private PlayerInput playerInput;
    private Rigidbody rb;
    private CharacterController cController;
    private InputAction jumpAction;
    private Vector3 moveInput;
    private Vector3 velocity;

    // Temp use to display different stages of jump
    private Renderer rend;

    private void Awake()
    {
        jumpAction = playerInput.actions["Jump"];
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        cController = GetComponent<CharacterController>();
        rend = GetComponent<Renderer>();
        rend.material.color = Color.green;
    }

    private void OnEnable()
    {
        jumpAction.started += OnJumpStarted;
        jumpAction.canceled += OnJumpReleased;
    }

    private void OnDisable()
    {
        jumpAction.started -= OnJumpStarted;
        jumpAction.canceled -= OnJumpReleased;
    }

    // Update is called once per frame
    void Update()
    {
        // Player movement
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        cController.Move(move * walkSpeed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        cController.Move(velocity * Time.deltaTime);

        // Jump display
        if (isCharging)
        {
            if (chargeProgress < 0.33f)
            {
                rend.material.color = Color.yellow;
            }
            else if (chargeProgress < 0.66f)
            {
                rend.material.color = new Color(1f, 0.5f, 0f);
            }
            else
            {
                rend.material.color = Color.red;
            }
        }
    }

    // Listener for movement input
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        float radians = moveAngleOffset * Mathf.Deg2Rad;

        // Create a rotation matrix for 2D vector
        float cos = Mathf.Cos(radians);
        float sin = Mathf.Sin(radians);

        // Rotate the moveInput vector
        Vector2 rotatedInput = new Vector2(
            moveInput.x * cos - moveInput.y * sin,
            moveInput.x * sin + moveInput.y * cos
        );

        moveInput = rotatedInput;
        Debug.Log("Move input: " + moveInput);
    }

    void OnJumpStarted(InputAction.CallbackContext ctx)
    {
        isCharging = true;
        jumpHoldTime = Time.time;
    }

    /// <summary>
    /// Handles the jumping logic
    /// </summary>
    /// <param name="ctx"></param>
    void OnJumpReleased(InputAction.CallbackContext ctx)
    {
        jumpHoldTime += Time.deltaTime;

        if (jumpHoldTime >= holdToStartCharge) isCharging = true;
        float finalJumpForce = jumpForce;

        // Charge jump
        if (isCharging)
        {
            
            chargeProgress = Mathf.Clamp((jumpHoldTime - holdToStartCharge) / fullChargeTime, 0f, 1f);
            float chargedForce = jumpForce * Mathf.Lerp(1f, stage3Multiplier, chargeProgress);

            if (chargeProgress < 0.33f)
            {
                finalJumpForce *= stage1Multiplier;
            }
            else if (chargeProgress < 0.66f)
            {
                finalJumpForce *= stage2Multiplier;
            }               
            else
            {
                finalJumpForce *= stage3Multiplier;
            }
        }
        Jump(finalJumpForce);
    }

    private void Jump(float force)
    {
        Debug.Log($"Jump with force: {force}");
        rb.AddForce(Vector3.up * force, ForceMode.Impulse);
        jumpHoldTime = 0f;
        isCharging = false;
        rend.material.color = Color.green;
    }
}
