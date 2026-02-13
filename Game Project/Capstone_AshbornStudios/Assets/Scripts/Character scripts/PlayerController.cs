using UnityEngine;

public class PlayerController: MonoBehaviour
{
    private CharacterController characterController;

    public float movementSpeed = 10f, climbMoveSpeed = 2f, rotationSpeed = 5f, jumpForce = 10f, climbForce = .5f, Gravity = -30f, diggingReach = 3f, damageVal = 10f;

    private float rotationY;
    private float verticalVelocity;
    public climbingTest test;
    private bool ground;
    float prevPos = 0;
    LayerMask blocksToDig;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        blocksToDig = LayerMask.GetMask("Diggable");
    }

    public void Move(Vector2 movementVector)
    {
        Vector3 move = transform.forward * movementVector.y + transform.right * movementVector.x;

        if (test.climbing && movementVector.y > 0)
        {
            move = move * climbMoveSpeed * Time.deltaTime;
            characterController.Move(move);
            verticalVelocity = climbForce;
            characterController.Move(new Vector3(0, verticalVelocity, 0) * Time.deltaTime);
        }
        else
        {
            
            move = move * movementSpeed * Time.deltaTime;
            characterController.Move(move);
            if (ground)
            {
                verticalVelocity = -0.1f;
            }
            else
            {
                verticalVelocity = verticalVelocity + Gravity * Time.deltaTime;
            }
            characterController.Move(new Vector3(0, Mathf.Clamp(verticalVelocity, -15f, 15f), 0) * Time.deltaTime);
        }
        
    }
    public void Rotate(Vector2 rotationVector)
    {
        rotationY += rotationVector.x * rotationSpeed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(0, rotationY, 0);
    }
    private void checkGround()
    {

        float currPos = transform.position.y;
        if(currPos == prevPos)
        {
            ground = true;
        }
        else
        {
            ground = false;
        }
        prevPos = currPos;
    }
    public void Jump()
    {
        if (characterController.isGrounded)
        {
            verticalVelocity = jumpForce;
        }
    }
    public void Dig()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, diggingReach, blocksToDig))
        {
            print("here");
            hit.transform.gameObject.GetComponent<diggableBlock>().hitBlock(damageVal);
        }
    }
}
