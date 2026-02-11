using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public PlayerController controller;
    private InputAction moveAction, lookAction, jumpAction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");
        jumpAction = InputSystem.actions.FindAction("Jump");

        jumpAction.performed += OnJumpPerformed;

        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movementVector = moveAction.ReadValue<Vector2>();
        controller.Move(movementVector);

        Vector2 lookVector = lookAction.ReadValue<Vector2>();
        controller.Rotate(lookVector);

    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        controller.Jump();
    }
}
