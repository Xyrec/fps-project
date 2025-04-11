using UnityEngine;

public class FPS : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private float jumpForce = 8f;  // Increased jump force
    [SerializeField] private float groundCheckDistance = 0.5f;  // Increased ground check distance

    private float verticalRotation = 0f;
    private float horizontalRotation = 0f;
    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        // Lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity = true;

        // Set the player's height (scale)
        transform.localScale = new Vector3(1f, 6f, 1f);  // Makes the player 6 units tall
    }

    // Update is called once per frame
    void Update()
    {
        // Handle mouse look
        HandleMouseLook();

        // Handle jumping
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }


    void FixedUpdate()
    {
        // Check if player is grounded
        CheckGrounded();

        // Handle WASD movement
        HandleMovement();
    }

    private void HandleMouseLook()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Calculate rotation
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        horizontalRotation += mouseX;

        // Apply rotation
        transform.localRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0f);
    }


    private void HandleMovement()
    {
        // Get input axes
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 moveDirection = transform.right * horizontalInput + transform.forward * verticalInput;

        // Apply movement using physics
        Vector3 velocity = moveDirection * moveSpeed;
        velocity.y = rb.linearVelocity.y; // Changed from linearVelocity to velocity
        rb.linearVelocity = velocity;
    }

    private void CheckGrounded()
    {
        // Create a slightly offset position to check from
        Vector3 origin = transform.position + Vector3.up * 0.1f;
        isGrounded = Physics.Raycast(origin, Vector3.down, groundCheckDistance + 0.2f);
    }

    private void OnDisable()
    {
        // Unlock cursor when script is disabled
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}