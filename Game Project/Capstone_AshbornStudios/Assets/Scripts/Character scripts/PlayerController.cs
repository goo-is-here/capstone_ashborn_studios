using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class PlayerController: MonoBehaviour
{
    private CharacterController characterController;

    public float movementSpeed = 10f, climbMoveSpeed = 2f, rotationSpeed = 1f, jumpForce = 10f, climbForce = .5f, Gravity = -30f, diggingReach = 3f, damageVal = 10f, durability = 25f, maxDurability = 25f;

    private float rotationY, rotationX;
    private float verticalVelocity;
    public climbingTest test;
    private bool ground;
    float prevPos = 0;
    public LayerMask blocksToDig;
    public GameObject cam;
    public bool isMoving;
    public bool isRunning;
    public bool isJumping;
    public bool isDigging;

    public TextMeshProUGUI text;
    GameObject textBox;
    GameObject durabilityBar;
    Image bar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxDurability = durability;
        textBox = GameObject.FindGameObjectWithTag("textBox");
        if(textBox != null)
        {
            text = textBox.GetComponent<TextMeshProUGUI>();
        }
        if(text != null)
        {
            text.gameObject.SetActive(false);
        }
        durabilityBar = GameObject.FindGameObjectWithTag("durability");
        if(durabilityBar != null)
        {
            bar = durabilityBar.GetComponent<Image>();
        }
        characterController = GetComponent<CharacterController>();
        blocksToDig = LayerMask.GetMask("Diggable");
        cam = GameObject.FindGameObjectWithTag("MainCamera");
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
        
        rotationY += Mathf.Clamp(rotationVector.x, -50, 50) * rotationSpeed * Time.deltaTime;
        rotationX -= Mathf.Clamp(rotationVector.y, -7, 7) * rotationSpeed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(0, rotationY, 0);
        cam.transform.localRotation = Quaternion.Euler(Mathf.Clamp(rotationX, -90f, 90f), 0, 0);
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
            isJumping = true;
        }
    }
    void Update()
    {
        isJumping = false;
        if(bar != null)
        {
            bar.fillAmount = durability / maxDurability;
        }
    }

    public void Dig()
    {
        if(durability > 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, diggingReach, blocksToDig))
            {
                hit.transform.gameObject.GetComponent<diggableBlock>().hitBlock(damageVal, hit.point);
            }
        }
        else
        {
            printText("Wait! I need to fix my tool!");
        }
    }
    public void printText(string textPrint)
    {
        if(text != null)
        {
            text.gameObject.SetActive(true);
            text.text = textPrint;
            StartCoroutine(wait());
        }
        
    }
    public float getDur()
    {
        return durability;
    }
    public void durabilityChange(float dur)
    {
        durability += dur;
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(5f);
        if(text != null)
        {
            text.gameObject.SetActive(false);
        }
        
    }
}
