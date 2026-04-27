using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;

    [Header("Movement")]
    public float movementSpeed = 10f;
    public float climbMoveSpeed = 2f;
    public float rotationSpeed = 1f;
    public float jumpForce = 10f;
    public float climbForce = .5f;
    public float Gravity = -30f;
    public float diggingReach = 3f;

    [Header("Tool Stats")]
    public float damageVal;
    public float durability;      
    public float maxDurability;  

    private float rotationY, rotationX;
    private float verticalVelocity;

    public climbingTest test;
    private bool ground;

    public LayerMask blocksToDig;
    public GameObject cam;

    public bool isMoving;
    public bool isRunning;
    public bool isJumping;
    public bool isDigging;

    public bool canMove = true;
    public TextMeshProUGUI text;
    GameObject textBox;

    GameObject durabilityBar;
    Image bar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        textBox = GameObject.FindGameObjectWithTag("textBox");
        if (textBox != null)
            text = textBox.GetComponent<TextMeshProUGUI>();

        if (text != null)
            text.gameObject.SetActive(false);

        durabilityBar = GameObject.FindGameObjectWithTag("durability");
        if (durabilityBar != null)
            bar = durabilityBar.GetComponent<Image>();

        characterController = GetComponent<CharacterController>();
        blocksToDig = LayerMask.GetMask("Diggable");
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }
    public int GetUsesLeft()
    {
        return Mathf.RoundToInt(durability);
    }

    public int GetMaxUses()
    {
        return Mathf.RoundToInt(maxDurability);
    }

   
    public void Move(Vector2 movementVector)
    {
        if (canMove)
        {
            ground = characterController.isGrounded;

            Vector3 move = transform.forward * movementVector.y + transform.right * movementVector.x;

            if (test.climbing && movementVector.y > 0)
            {
                move *= climbMoveSpeed * Time.deltaTime;
                characterController.Move(move);

                verticalVelocity = climbForce;
                characterController.Move(Vector3.up * verticalVelocity * Time.deltaTime);
            }
            else
            {
                move *= movementSpeed * Time.deltaTime;
                characterController.Move(move);

                if (ground && verticalVelocity < 0)
                    verticalVelocity = -2f;
                else
                    verticalVelocity += Gravity * Time.deltaTime;

                characterController.Move(Vector3.up * verticalVelocity * Time.deltaTime);
            }
        }
        
    }

    public void Rotate(Vector2 rotationVector)
    {
        if (canMove)
        {
            rotationY += Mathf.Clamp(rotationVector.x, -50, 50) * rotationSpeed * Time.deltaTime;
            rotationX -= Mathf.Clamp(rotationVector.y, -7, 7) * rotationSpeed * Time.deltaTime;

            transform.localRotation = Quaternion.Euler(0, rotationY, 0);
            cam.transform.localRotation = Quaternion.Euler(Mathf.Clamp(rotationX, -90f, 90f), 0, 0);
        }
        
    }

    public void Jump()
    {
        if (characterController.isGrounded && canMove)
        {
            verticalVelocity = jumpForce;
            isJumping = true;
        }
    }

    void Update()
    {
        isJumping = false;

        if (bar != null)
            bar.fillAmount = durability / maxDurability;
    }

    public void Dig()
    {
        if (canMove)
        {

            if (durability > 0)
            {
                print("here");
                RaycastHit hit;
                if (Physics.Raycast(transform.position, cam.transform.forward, out hit, diggingReach, blocksToDig))
                {
                    hit.transform.gameObject.GetComponent<diggableBlock>().hitBlock(damageVal, hit.point);
                }
            }
            else
            {
                printText("My tool is broken!");
            }
        }
    }

    public void durabilityChange(float dur)
    {
        durability += dur;
        durability = Mathf.Clamp(durability, 0f, maxDurability);
    }

 
    public void printText(string textPrint)
    {
        if (text != null)
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

    IEnumerator wait()
    {
        yield return new WaitForSeconds(5f);
        if (text != null)
            text.gameObject.SetActive(false);
    }
}