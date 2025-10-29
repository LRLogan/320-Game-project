using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    public bool canMove = true;

    // Movement 
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 5f;
    private float moveAngleOffset = 45f;
    private bool isRunning = false;
    public bool rotateControls = false;

    [Header("Jumping")]
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float stage1Multiplier = 1.2f;
    [SerializeField] private float stage2Multiplier = 1.6f;
    [SerializeField] private float stage3Multiplier = 2f;
    [SerializeField] private float holdToStartCharge = 1f;
    [SerializeField] private float fullChargeTime = 3f;
    private float jumpStartTime = 0f;
    private float chargeProgress = 0f;
    private bool isGrounded = true;
    private bool isCharging = false;

    //interactions
    [SerializeField] private GameObject interact;
    private bool isInteracting = false;
    int interactTimer = 0;
    int maxInteractTimer = 10;
    GameObject interactField;
    private InputAction interactAction;
    private Vector3 direction;

    // Player object
    [SerializeField] private PlayerInput playerInput;
    private Player playerInstance;
    private Rigidbody rb;
    private InputAction jumpAction;
    private Vector3 moveInput;

    // Temp use to display different stages of jump
    private Renderer rend;

    [SerializeField] private LayerMask groundLayer;
    public InventoryManager inventoryUI;
    [SerializeField] private  List<GameObject> inventory;
    public bool isInside = false;
    public Vector3 posBeforeSceneChange;

    private void Awake()
    {
        if (playerInstance == null)
        {
            playerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (playerInstance != this)
        {
            Destroy(gameObject);
        }

        jumpAction = playerInput.actions["Jump"];
       
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rend = GetComponent<Renderer>();
        rend.material.color = Color.green;
        inventory = new List<GameObject>();
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
        if (!canMove)
            return;

        // Player movement
        Vector3 move;
        if (isRunning)
        {
            move = moveInput.normalized * walkSpeed * 1.7f;
        }
        else
        {
            move = moveInput.normalized * walkSpeed;
        }
        Vector3 newVelocity = new Vector3(move.x, rb.velocity.y, move.z);
        rb.velocity = newVelocity;

        if (newVelocity != Vector3.zero)
        {
            transform.forward = newVelocity;
        }
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
       
        if (isInteracting && interactTimer < maxInteractTimer)
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

    public void OnSetRun(InputAction.CallbackContext context)
    {
        if(context.started || context.performed)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!canMove)
            return;

        Vector2 rawInput = context.ReadValue<Vector2>();
        Vector3 localInput = new Vector3(rawInput.x, 0, rawInput.y);
        Quaternion offsetRot;

        // Rotate controls when the cam angle switches 
        if (rotateControls)
        {
            offsetRot = Quaternion.Euler(0, moveAngleOffset * 3 - 15, 0);
        }
        else
        {
            offsetRot = Quaternion.Euler(0, moveAngleOffset, 0);
        }

        moveInput = offsetRot * localInput;
        if(moveInput != Vector3.zero)
        {
            moveInput.Normalize();
            direction = moveInput;
        }
       
        //Debug.Log("Move input: " + moveInput);
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!canMove)
            return;

        if (isInteracting == false && context.started)
        {

            float xPosition = transform.position.x;
            float yPosition = transform.position.y;
            interactField = Instantiate(
           interact,
           new Vector3(xPosition + direction.x, yPosition + direction.y, transform.position.z + direction.z),
           Quaternion.identity);
            isInteracting = true;

            interactTimer = 0;
        }
    }

    void OnJumpStarted(InputAction.CallbackContext ctx)
    {
        if (!canMove)
            return;

        if (isGrounded)
        {
            isCharging = true;
            jumpStartTime = Time.time;
        }
    }

    // Hnadels jump logic
    void OnJumpReleased(InputAction.CallbackContext ctx)
    {
        if (!canMove || !isGrounded) return;

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
        if (!canMove)
            return;

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

    /// <summary>
    /// Uisng a method to access inventory so additional functionality can be added where needed with actual inventory functionality
    /// </summary>
    /// <param name="newObj"></param>
    /// <returns></returns>
    public bool AddToInventory(GameObject newObj)
    {
        Debug.Log("NewOBJ picked up is null: " + newObj == null);
        if (newObj != null)
        {
            inventory.Add(newObj);
            inventoryUI.RefreshUI();
            Debug.Log("Added to inventory: " + newObj);
            return true;
        }
        return false;
    }

    public List<GameObject> GetInventory()
    {
        return inventory;
    }
}
