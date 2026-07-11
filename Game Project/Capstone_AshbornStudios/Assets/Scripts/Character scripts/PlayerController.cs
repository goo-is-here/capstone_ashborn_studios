using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour, IDataPersistence
{
    private CharacterController characterController;

    [Header("Movement Variables")]
    public float movementSpeed = 10f;
    public float climbMoveSpeed = 2f;
    public float rotationSpeed = 1f;
    public float jumpForce = 10f;
    public float climbForce = .5f;
    public float Gravity = -30f;
    

    [Header("Tool Stats")]
    public float damageVal;
    public float durability;      
    public float maxDurability;
    public float diggingReach = 3f;

    private float rotationY, rotationX;
    private float verticalVelocity;

    [Header("Foot Object")]
    public climbingTest climbingObject;
    private bool ground;

    [Header("Diggable Blocks Layer")]
    public LayerMask blocksToDig;
    [Header("Camera Object")]
    public GameObject cam;

    public characterInventory inventory;

    [HideInInspector]
    public bool isMoving;
    [HideInInspector]
    public bool isRunning;
    [HideInInspector]
    public bool isJumping;
    [HideInInspector]
    public bool isDigging;
    [HideInInspector]
    public bool canMove = false;

    [Header("Text Box Object")]
    public TextMeshProUGUI text;
    GameObject textBox;

    GameObject durabilityBar;
    Image bar;

    [SerializeField] AudioClip defaultDig;
    [SerializeField] AudioClip toolBreak;
    [SerializeField] AudioClip walking;
    AudioSource audioSource;
    enum sceneType { HUB, MINE};
    sceneType thisEnum = sceneType.HUB;
    public Vector3 pos;
    bool canDig = true;
    bool playSound = true;
    bool tele = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        //grabs this objects characterController script, it is required so it SHOULD never be null
        characterController = GetComponent<CharacterController>();
        if(characterController == null)
        {
            Debug.LogError("Character controller not added");
        }
        //must set manually
        if(climbingObject == null)
        {
            Debug.LogError("Foot not set");
        }
    }
    void Start()
    {
        //grabs text to print to
        textBox = GameObject.FindGameObjectWithTag("textBox");
        if (textBox != null)
            text = textBox.GetComponent<TextMeshProUGUI>();
        //if text is not null, turns the text off
        if (text != null)
            text.gameObject.SetActive(false);
        //gets the durability bar, if it exists in the scene gets the image component
        durabilityBar = GameObject.FindGameObjectWithTag("durability");
        if (durabilityBar != null)
            bar = durabilityBar.GetComponent<Image>();

        //sets the layer mask for diggable blocks
        blocksToDig = LayerMask.GetMask("Diggable");
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        //sets the scene enum for saving and loading
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            this.thisEnum = sceneType.HUB;
        }
        else
        {
            this.thisEnum = sceneType.MINE;
        }
        //gets the audio source component from the player object
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }
    //gets the durability
    public int GetUsesLeft()
    {
        return Mathf.RoundToInt(durability);
    }
    //Load data. Read variables from game data into variables here
    //Currently saving current scene, position per scene, damage of tool, durability of tool, max durability
    public void LoadData(GameData data)
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            pos = data.hubPosition;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            pos = data.minePosition;
        }

        damageVal = data.damageVal;
        durability = data.durability;
        maxDurability = data.maxDurability;
        StartCoroutine(ApplyLoadedPosition());
    }
    //save variables into game data
    //Currently saving current scene, position per scene, damage of tool, durability of tool, max durability
    public void SaveData(ref GameData data)
    {
        if(data != null)
        {
            
            data.scenceIndex = SceneManager.GetActiveScene().buildIndex;
            data.damageVal = this.damageVal;
            data.durability = this.durability;
            data.maxDurability = this.maxDurability;
            if (thisEnum == sceneType.HUB)
            {
                data.hubPosition = pos;
            }
            else if (thisEnum == sceneType.MINE)
            {
                data.minePosition = pos;
                print(data.minePosition);
            }
        }
        
    }
    //gets max uses of the tool
    public int GetMaxUses()
    {
        return Mathf.RoundToInt(maxDurability);
    }

   //this moves the player with the character controller
    public void Move(Vector2 movementVector)
    {
        //checks if player has stopped
        if(movementVector.x == 0f && movementVector.y == 0f)
        {
            isMoving = false;
            playSound = true;
        }
        else
        {
            //tries to make sure only one footstep is playing at a time
            if (playSound)
            {
                playSound = false;
                audioSource.PlayOneShot(walking);
                StartCoroutine(soundRepeat(walking.length));

            }
            
            isMoving = true;
        }
        //checks if nothing is stopping the player from moving such as a menu
        if (canMove)
        {
            //checks if grounded and gets the forces
            ground = characterController.isGrounded;
            Vector3 move = transform.forward * movementVector.y + transform.right * movementVector.x;

            //climbing movement here to allow for vertical movement and a lowered movement speed
            if (climbingObject.climbing && movementVector.y > 0)
            {
                move *= climbMoveSpeed * Time.deltaTime;
                characterController.Move(move);

                verticalVelocity = climbForce;
                characterController.Move(Vector3.up * verticalVelocity * Time.deltaTime);
            }
            //normal grounded movement
            else
            {
                move *= movementSpeed * Time.deltaTime;
                characterController.Move(move);
                //falling logic
                if (ground && verticalVelocity < 0)
                    verticalVelocity = -2f;
                else
                    verticalVelocity += Gravity * Time.deltaTime;

                characterController.Move(Vector3.up * verticalVelocity * Time.deltaTime);
            }
        }
        
    }
    //rotates the player object and camera
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
    //jump function, can only jump when grounded and when you can move
    public void Jump()
    {
        if (characterController.isGrounded && canMove)
        {
            verticalVelocity = jumpForce;
            isJumping = true;
        }
    }
    //update
    void Update()
    {
        isJumping = false;

        if (bar != null)
            bar.fillAmount = durability / maxDurability;
    }
    //late update for movement to assist with camera stuttering
    private void LateUpdate()
    {
        pos = transform.localPosition;
    }
    //dig function
    public void Dig()
    {
        //must be able to both move and dig. digging may be disabled when tool is broken
        if (canMove && canDig)
        {
            //checks durability
            if (durability > 0)
            {
                RaycastHit hit;
                //uses a raycast to find the block to dig
                if (Physics.Raycast(transform.position, cam.transform.forward, out hit, diggingReach, blocksToDig))
                {
                    canDig = false;
                    hit.transform.gameObject.GetComponent<diggableBlock>().hitBlock(damageVal, hit.point);
                    //plays the digging audio
                    AudioClip clip = hit.transform.gameObject.GetComponent<diggableBlock>().getDigClip();
                    if(clip == null)
                    {
                        audioSource.PlayOneShot(defaultDig);
                        float clipLength = defaultDig.length;
                        StartCoroutine(canDigTrue(clipLength/3));
                    }
                    else
                    {
                        audioSource.PlayOneShot(clip);
                        float clipLength = clip.length;
                        StartCoroutine(canDigTrue(clipLength/3));
                    }
                }
            }
            //announces that the tool is broken and sets the dig value lower
            else
            {
                printText("My tool is broken!");
                damageVal = 7;
            }
        }
    }
    //updates the tool durability
    public void durabilityChange(float dur)
    {
        durability += dur;
        //makes sure the durability never goes below zero and if it breaks, plays the sound
        durability = Mathf.Clamp(durability, 0f, maxDurability);
        if(durability == 0)
        {
            audioSource.PlayOneShot(toolBreak);
        }
    }

    //just prints a given string to the text box
    public void printText(string textPrint)
    {
        if (text != null)
        {
            text.gameObject.SetActive(true);
            text.text = textPrint;
            StartCoroutine(wait());
        }
    }
    //gets the durability
    //TODO remove duplicate durability function
    public float getDur()
    {
        return durability;
    }

    public void addItemInventory(Item ite)
    {
        inventory.addItem(ite);
    }
    //a set wait time for functions, default time is 5 seconds
    IEnumerator wait(float time = 5)
    {
        yield return new WaitForSeconds(time);
        if (text != null)
            text.gameObject.SetActive(false);
    }
    //applies a dig cooldown
    IEnumerator canDigTrue(float length)
    {
        yield return new WaitForSeconds(length);
        canDig = true;
    }
    //applies a sound cooldown
    IEnumerator soundRepeat(float length)
    {
        yield return new WaitForSeconds(length);
        playSound = true;
    }
    //helps with loaded position when saving
    //TODO find a better way to do this
    IEnumerator ApplyLoadedPosition()
    {
            canMove = false;

            characterController.enabled = false;

            yield return null;

            transform.localPosition = pos;
            verticalVelocity = 0f;

            yield return null;

            characterController.enabled = true;
            transform.localPosition = pos;

            yield return null;

            canMove = true;

        
    }
}