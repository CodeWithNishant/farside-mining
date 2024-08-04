using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6.0f;
    public float gravity = -9.8f;
    public float jumpHeight = 1.0f;
    public Transform cameraTransform;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Check if the player is on the ground
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Get input for movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Calculate the movement direction
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Move the character
        controller.Move(move * speed * Time.deltaTime);

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Mouse look
        //float mouseX = Input.GetAxis("Mouse X");
        //float mouseY = Input.GetAxis("Mouse Y");

        //cameraTransform.Rotate(Vector3.up * mouseX);
        //cameraTransform.Rotate(Vector3.left * mouseY);
    }
}
