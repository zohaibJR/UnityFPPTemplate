using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float sprintSpeed = 8f;
    public float jumpHeight = 2f;

    [Header("Gravity")]
    public float gravity = -20f;
    public float fallMultiplier = 2f;

    [Header("Mouse Look")]
    public Transform cameraHolder;
    public float mouseSensitivity = 200f;

    private CharacterController controller;

    private Vector3 velocity;
    private bool isGrounded;

    private float xRotation = 0f;

    [Header("Stamina")]
    public float maxStamina = 5f;
    public float currentStamina;
    public float staminaDrain = 1f;
    public float staminaRegen = 1.5f;

    public UnityEngine.UI.Image staminaFillImage;


    void Start()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        currentStamina = maxStamina;
    }

    void Update()
    {
        GroundCheck();
        Move();
        MouseLook();
        ApplyGravity();
        HandleStamina();
    }

    void GroundCheck()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        bool canSprint = Input.GetKey(KeyCode.LeftShift) && currentStamina > 0;

        float currentSpeed = canSprint ? sprintSpeed : moveSpeed;

        controller.Move(move * currentSpeed * Time.deltaTime);

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    void ApplyGravity()
    {
        // Better falling
        if (velocity.y < 0)
        {
            velocity.y += gravity * fallMultiplier * Time.deltaTime;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        controller.Move(velocity * Time.deltaTime);
    }

    void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        cameraHolder.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleStamina()
    {
        bool isSprinting = Input.GetKey(KeyCode.LeftShift) && isGrounded && currentStamina > 0;

        if (isSprinting)
        {
            currentStamina -= staminaDrain * Time.deltaTime;

            if (currentStamina < 0)
                currentStamina = 0;
        }
        else
        {
            currentStamina += staminaRegen * Time.deltaTime;

            if (currentStamina > maxStamina)
                currentStamina = maxStamina;
        }

        UpdateStaminaUI();
    }

    void UpdateStaminaUI()
    {
        if (staminaFillImage != null)
        {
            staminaFillImage.fillAmount = currentStamina / maxStamina;
        }
    }
}