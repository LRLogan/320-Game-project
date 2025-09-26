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

    [Header("Jumping")]
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float stage1Multiplier = 1.2f;
    [SerializeField] private float stage2Multiplier = 1.6f;
    [SerializeField] private float stage3Multiplier = 2f;
    [SerializeField] private float holdToStartCharge = 1f;
    [SerializeField] private float fullChargeTime = 3f;
    [SerializeField] private float gravity = -9.8f;

    [SerializeField] private GameObject interact;

    private float jumpHoldTime = 0f;
    private bool isCharging = false;
    private bool jumpButtonPressed = false;
    private bool isGrounded;

    private bool isInteracting = false;

    // Player object
    private Rigidbody rb;
    private CharacterController cController;
    private Vector3 moveInput;
    private Vector3 velocity;

    // Temp use to display different stages of jump
    private Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        cController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Handeling jump input

        if(jumpButtonPressed)
        {
            jumpHoldTime += Time.deltaTime;

            if(jumpHoldTime >= holdToStartCharge) isCharging=true;

            // Temp display to show different jump power stages
            if(isCharging)
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

        // Jump release
        if(true)
        {

        }
        Debug.Log("Move input: " + moveInput);
        if (Input.GetKeyDown("E"))
        {
            Debug.Log("Move input: " + moveInput);
            velocity.y = Mathf.Sqrt(2 * -2f * gravity);
            float xPosition = transform.position.x;
            float yPosition = transform.position.y;
                 GameObject Spawnedball = Instantiate(
                interact,
                new Vector3(xPosition, yPosition, transform.position.z),
                Quaternion.identity);
        }
           

        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        cController.Move(move * walkSpeed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        cController.Move(velocity * Time.deltaTime);
    }

    // Listener for jump button input
    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.performed && cController.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            jumpButtonPressed = true;
        }
    }

    // Listener for movement input
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log("Move input: " + moveInput);
    }

    //public void onInteract(InputAction.CallbackContext context)
    //{
     //   isInteracting = true;
   // }
}
