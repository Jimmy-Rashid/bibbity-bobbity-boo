using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public GameObject freeCamera;
    public float movementSpeed = 15f;
    public float jumpHeight = 10;
    private Rigidbody rb;
    public PlayerInputActions playerControls;
    private Vector2 moveInput;
    private float jumpInput;
    private Vector2 directionInput;
    private Vector3 moveDirection;
    private Vector3 cameraForward;
    private Vector3 cameraRight;

    // mental illness variables
    PlayerGrind grindScript;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
        playerControls.Enable();
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void OnMove(InputValue val)
    {
        moveInput = val.Get<Vector2>();
    }

    private void OnJump(InputValue val)
    {
        if (val.isPressed)
        {
            if (GetComponent<PlayerGrind>().onRail)
            {
                Debug.Log("Trying to jump on rail :3c");
                GetComponent<PlayerGrind>().FeetCollisionOnRail(); // if on rail, call FeetCollisionOnRail to reset rail
                rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            }
            else if (GetComponent<FeetCollision>().isGrounded) // if jump is pressed and player is grounded
            {
                rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            }
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        grindScript = GetComponent<PlayerGrind>();
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (!grindScript.onRail)
        { // controls for when on ground
            cameraForward = freeCamera.transform.forward;
            cameraRight = freeCamera.transform.right;

            cameraForward.y = 0;
            cameraRight.y = 0;

            cameraForward.Normalize();
            cameraRight.Normalize();

            moveDirection = (moveInput.x * cameraRight) + (moveInput.y * cameraForward); // calc for direction of movement 
            transform.LookAt(new Vector3(moveDirection.x, 0, moveDirection.z) + transform.position); // look in direction

            rb.linearVelocity = new Vector3(moveDirection.x * movementSpeed, rb.linearVelocity.y, moveDirection.z * movementSpeed);
        }
    }
}
