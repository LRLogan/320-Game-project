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
    private float moveAngleOffset = 45f;

    [Header("Jumping")]
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float stage1Multiplier = 1.2f;
    [SerializeField] private float stage2Multiplier = 1.6f;
    [SerializeField] private float stage3Multiplier = 2f;
    [SerializeField] private float holdToStartCharge = 1f;
    [SerializeField] private float fullChargeTime = 3f;
    [SerializeField] private float gravity = -9f;
    private float jumpStartTime = 0f;
    private float chargeProgress = 0f;
    private bool isGrounded = true;
    private bool isCharging = false;

    [SerializeField] private GameObject interact;
    private bool isInteracting = false;
    int interactTimer = 0;
    int maxInteractTimer = 10;
    GameObject interactField;

    // Player object
    [SerializeField] private PlayerInput playerInput;
    private Rigidbody rb;
    private InputAction jumpAction;
    private Vector3 moveInput;

    // Temp use to display different stages of jump
    private Renderer rend;

    [SerializeField] private LayerMask groundLayer;
    private List<GameObject> inventory;

    private void Awake()
    {
        jumpAction = playerInput.actions["Jump"];
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
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
    private void FixedUpdate()
    {
        // Player movement
        Vector3 move = moveInput.normalized * walkSpeed;
        Vector3 newVelocity = new Vector3(move.x, rb.velocity.y, move.z);
        rb.velocity = newVelocity;
    }

    void Update()
    {
        // Jump display (only update while charging)
        if (isCharging)
        {
            float heldDuration = Time.time - jumpStartTime;

            // Adjusted so fullChargeTime = total time to max charge
            chargeProgress = Mathf.Clamp(heldDuration / fullChargeTime, 0f, 1f);

            if (chargeProgress < 0.33f)
            {
                rend.material.color = Color.yellow;
            }
            else if (chargeProgress < 0.66f)
            {
                rend.material.color = new Color(1f, 0.5f, 0f); // orange
            }
            else
            {
                rend.material.color = Color.red;
            }
        }
        if (Input.GetKeyDown(KeyCode.E) && isInteracting == false)
        {

            float xPosition = transform.position.x;
            float yPosition = transform.position.y;
            interactField = Instantiate(
           interact,
           new Vector3(xPosition, yPosition, transform.position.z),
           Quaternion.identity);
            isInteracting = true;

            interactTimer = 0;
        }
        else if (isInteracting && interactTimer < maxInteractTimer)
        {
            interactTimer++;
        }
        else if (isInteracting && interactTimer >= maxInteractTimer)
        {
            GameObject temp = interactField;
            interactField = null;
            Destroy(temp);
            isInteracting = false;
        }

    }



    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 rawInput = context.ReadValue<Vector2>();

        Vector3 localInput = new Vector3(rawInput.x, 0, rawInput.y);
        Quaternion offsetRot = Quaternion.Euler(0, moveAngleOffset, 0);
        moveInput = offsetRot * localInput;

        Debug.Log("Move input: " + moveInput);
    }

    void OnJumpStarted(InputAction.CallbackContext ctx)
    {
        if (isGrounded)
        {
            isCharging = true;
            jumpStartTime = Time.time;
        }
    }

    // Hnadels jump logic
    void OnJumpReleased(InputAction.CallbackContext ctx)
    {
        if (!isGrounded) return;

        float heldDuration = Time.time - jumpStartTime;
        isCharging = false;

        float finalJumpForce = jumpForce;

        if (heldDuration >= holdToStartCharge)
        {
            // Adjusted so total charge time = fullChargeTime
            chargeProgress = Mathf.Clamp(heldDuration / fullChargeTime, 0f, 1f);

            if (chargeProgress < 0.33f)
                finalJumpForce *= stage1Multiplier;
            else if (chargeProgress < 0.66f)
                finalJumpForce *= stage2Multiplier;
            else
                finalJumpForce *= stage3Multiplier;
        }
        Jump(finalJumpForce);
    }

    private void Jump(float force)
    {
        Debug.Log($"Jump with force: {force}");
        rb.AddForce(Vector3.up * force, ForceMode.Impulse);
        jumpStartTime = 0f;
        rend.material.color = Color.green;
    }

    // Ground check --------------
    private void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = false;
        }
    }
    // ---------------------------


}
