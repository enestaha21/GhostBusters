using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float sprintSpeed = 9f;
    public float gravity = -9.81f;
    public float mouseSensitivity = 2f;
    public Transform playerCamera;

    [Header("Footstep Settings")]
    public AudioSource footstepSource;
    public AudioClip dirtFootstep;
    public AudioClip woodFootstep;
    public float walkStepInterval = 0.5f;
    public float sprintStepInterval = 0.3f;

    private CharacterController controller;
    private Vector3 velocity;
    private float xRotation = 0f;
    private bool isSprinting = false;
    private float stepTimer = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)) isSprinting = !isSprinting;
        float speed = isSprinting ? sprintSpeed : walkSpeed;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

       
        bool isMoving = (Mathf.Abs(x) > 0.1f || Mathf.Abs(z) > 0.1f);
        HandleFootsteps(isMoving);
    }

    private void HandleFootsteps(bool isMoving)
    {
        if (controller.isGrounded && isMoving)
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0f)
            {
                PlayFootstepSound();
                stepTimer = isSprinting ? sprintStepInterval : walkStepInterval;
            }
        }
        else
        {
            stepTimer = 0f;

            
            if (footstepSource != null && footstepSource.isPlaying)
            {
                footstepSource.Stop();
            }
        }
    }

    private void PlayFootstepSound()
    {
        AudioClip clipToPlay = dirtFootstep;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 2f))
        {
            if (hit.collider.CompareTag("WoodFloor"))
            {
                clipToPlay = woodFootstep;
            }
        }

        if (footstepSource != null && clipToPlay != null)
        {
            
            footstepSource.clip = clipToPlay;
            footstepSource.Play();
        }
    }
}