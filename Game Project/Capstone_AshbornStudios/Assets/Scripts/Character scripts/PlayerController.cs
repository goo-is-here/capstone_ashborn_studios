using UnityEngine;

public class PlayerController: MonoBehaviour
{
    private CharacterController characterController;

    public float movementSpeed = 10f, rotationSpeed = 5f, jumpForce = 10f, Gravity = -30f;

    private float rotationY;
    private float verticalVelocity;
    public climbingTest test;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void Move(Vector2 movementVector)
    {
        Vector3 move = transform.forward * movementVector.y + transform.right * movementVector.x;
        move = move * movementSpeed * Time.deltaTime;
        characterController.Move(move);

        if (test.climbing && movementVector.y > 0)
        {
            verticalVelocity = jumpForce;
            characterController.Move(new Vector3(0, verticalVelocity, 0) * Time.deltaTime);
        }
        else
        {
            verticalVelocity = verticalVelocity + Gravity * Time.deltaTime;
            characterController.Move(new Vector3(0, verticalVelocity, 0) * Time.deltaTime);
        }
        
    }
    public void Rotate(Vector2 rotationVector)
    {
        rotationY += rotationVector.x * rotationSpeed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(0, rotationY, 0);
    }
    public void Jump()
    {
        if (characterController.isGrounded)
        {
            verticalVelocity = jumpForce;
        }
    }
}
