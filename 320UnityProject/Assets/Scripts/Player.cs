using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private float jumpHoldTime = 0f;
    private bool isCharging = false;
    private bool jumpButtonHeld = false;
    private bool isGrounded;

    // Player object
    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
