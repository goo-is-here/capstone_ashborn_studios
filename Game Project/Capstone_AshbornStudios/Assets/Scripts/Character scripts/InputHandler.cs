using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public PlayerController controller;
    private InputAction moveAction, lookAction, jumpAction, digAction;
    Vector2 movementVector;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");
        jumpAction = InputSystem.actions.FindAction("Jump");
        digAction = InputSystem.actions.FindAction("Attack");
    }
    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        movementVector = moveAction.ReadValue<Vector2>();
        controller.Move(movementVector);

        Vector2 lookVector = lookAction.ReadValue<Vector2>();
        controller.Rotate(lookVector);

    }
    private void OnEnable()
    {
        digAction.performed += OnDigPerformed;
        jumpAction.performed += OnJumpPerformed;
    }
    private void OnDisable()
    {
        digAction.performed -= OnDigPerformed;
        jumpAction.performed -= OnJumpPerformed;
    }
    public float getForward()
    {
        return movementVector.y;
    }
    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        controller.Jump();
    }
    private void OnDigPerformed(InputAction.CallbackContext context)
    {
        controller.Dig();
    }
}
