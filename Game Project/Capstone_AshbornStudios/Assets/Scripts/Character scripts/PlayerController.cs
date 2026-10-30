using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

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
    public float diggingRange = 5f;
    private float rotationY, rotationX;
    private float verticalVelocity;
    public float mineSpeed = 1f;

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
    public SpawnMineralPopup mineralPopupSpawner;
    GameObject durabilityBar;
    Image bar;

    [SerializeField] AudioClip defaultDig;
    [SerializeField] AudioClip toolBreak;
    [SerializeField] AudioClip walking;
    [SerializeField] AudioClip mineralPickup;
    [SerializeField] AudioClip lanternEquip;
    [SerializeField] AudioClip lanternUnequip;
    [SerializeField] AudioClip[] digSounds;
    [SerializeField] AudioSource sfxAudioSource;
    [SerializeField] AudioSource bagAudioSource;

    
    enum sceneType { HUB, MINE};
    sceneType thisEnum = sceneType.HUB;
    public Vector3 pos;
    bool canDig = true;
    bool playSound = true;
    bool isPlayingSound = false;
    bool tele = false;
    bool canMine = true;
    [HideInInspector]
    public bool[] collectedGems = new bool[16];
    public float additionalOreChance = 0f;
    private Animator anim;
    [SerializeField] GameObject[] clawsLeft;
    [SerializeField] GameObject[] clawsRight;
    public int clawIndex = 0;

    [Header("Lantern Reference")]
    public Lantern lantern;

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
        //gets the animator in children
        anim = GetComponentInChildren<Animator>();
        setClaw();
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

        collectedGems = data.collectedGems;
        damageVal = data.damageVal;
        durability = data.durability;
        maxDurability = data.maxDurability;
        StartCoroutine(ApplyLoadedPosition());
        clawIndex = data.clawIndex;
    }
    void setClaw()
    {
        for(int i = 0; i < clawsLeft.Length; i++)
        {
            clawsLeft[i].SetActive(false);
            clawsRight[i].SetActive(false);
        }
        clawsLeft[clawIndex].SetActive(true);
        clawsRight[clawIndex].SetActive(true);
    }
    public void upgradeClaws()
    {
        print("here");
        clawsLeft[clawIndex].SetActive(false);
        clawsRight[clawIndex].SetActive(false);
        clawIndex++;
        Mathf.Clamp(clawIndex, 0, clawsLeft.Length - 1);
        clawsLeft[clawIndex].SetActive(true);
        clawsRight[clawIndex].SetActive(true);
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
            }
            data.clawIndex = clawIndex;
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

            if (!isPlayingSound)
            {
                playSound = true;
            }
            bagAudioSource.loop = false;
            bagAudioSource.Stop();
        }
        else
        {
            //tries to make sure only one footstep is playing at a time
            if (playSound)
            {
                playSound = false;
                isPlayingSound = true;
                
                sfxAudioSource.PlayOneShot(walking);
                StartCoroutine(soundRepeat(walking.length));
                if (!bagAudioSource.isPlaying)
                {
                    bagAudioSource.loop = true;
                    bagAudioSource.Play();
                }
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
        if (Input.GetMouseButton(0) && canMine && canMove)
        {
            anim.SetTrigger("UseTool");
            StartCoroutine(mineCooldown());
        }
    }
    //late update for movement to assist with camera stuttering
    private void LateUpdate()
    {
        pos = transform.localPosition;
    }
    //dig function
    public void Dig()
    {
        
    }

    //updates the tool durability
    public void durabilityChange(float dur)
    {
        durability += dur;
        //makes sure the durability never goes below zero and if it breaks, plays the sound
        durability = Mathf.Clamp(durability, 0f, maxDurability);
        if(durability == 0)
        {
            sfxAudioSource.PlayOneShot(toolBreak);
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

    public void addItemInventory(Item ite, pickUpItem obj = null)
    {
        float rand = Random.Range(0f, 1f);
        bool triggeredDoubleChance = false;

        if(rand < additionalOreChance/100)
        {
            inventory.addItem(ite, obj);
            triggeredDoubleChance = true;
        }

        inventory.addItem(ite, obj);
        sfxAudioSource.PlayOneShot(mineralPickup);

        if (triggeredDoubleChance)
        {
            mineralPopupSpawner.DisplayCollectible(ite.itemName, ite.icon, 2);
        }
        else
        {
            mineralPopupSpawner.DisplayCollectible(ite.itemName, ite.icon, 1);
        }
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
        isPlayingSound = false;
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
    
    private Mesh mesh;
    private Vector3[] vertices;
    private IEnumerator mineCooldown()
    {
        //check to see if hit dirt this dig
        bool hitDirt = false;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, diggingReach, blocksToDig))
        {
            Collider[] intersecting = Physics.OverlapSphere(hitInfo.point, diggingRange);
            foreach (Collider col in intersecting)
            {
                if (col.CompareTag("Dirt"))
                {
                    hitDirt = true;
                }
            }

        }

        //play dig sound 1
        int rand = Random.Range(0, digSounds.Length - 1);
        int lastSound = rand;
        AudioClip soundToPlay = digSounds[rand];
        if (hitDirt)sfxAudioSource.PlayOneShot(soundToPlay);

        canMine = false;
        yield return new WaitForSeconds(mineSpeed / 2);
        
        //play dig sound 2
        rand = Random.Range(0, digSounds.Length - 1);
        if (rand == lastSound)
        {
            if (rand == digSounds.Length - 1)
                rand -= 1;
            else
                rand += 1;
        }
        AudioClip secondSoundToPlay = digSounds[rand];
        if(hitDirt) sfxAudioSource.PlayOneShot(secondSoundToPlay);


        if (Physics.Raycast(ray, out hitInfo, diggingReach, blocksToDig))
        {
            Collider[] intersecting = Physics.OverlapSphere(hitInfo.point, diggingRange);
            foreach(Collider col in intersecting)
            {
                if (col.CompareTag("Dirt"))
                {
                    col.gameObject.GetComponent<dirt>().breakObject();
                }
            }
            Collider[] intersecting2 = Physics.OverlapBox(hitInfo.point, new Vector3(0.5f, 0.75f, 0.1f));
            foreach (Collider col in intersecting2)
            {
                if (col.CompareTag("Dirt"))
                {
                    col.gameObject.GetComponent<dirt>().breakObject();
                }
            }
        }
        yield return new WaitForSeconds(mineSpeed / 2);
        canMine = true;
    }

    public void CollectGem(int gemID)
    {
        collectedGems[gemID] = true;
    }

    public void EquipLantern()
    {
        if (lantern.lanternUnlocked)
        {
            lantern.GetComponent<MeshRenderer>().enabled = true;
            lantern.GetComponent<Light>().enabled = true;
            sfxAudioSource.PlayOneShot(lanternEquip);
        }
    }

    public void UnequipLantern()
    {
        lantern.GetComponent<MeshRenderer>().enabled = false;
        lantern.GetComponent<Light>().enabled = false;
        if (lantern.lanternUnlocked)
        {
            sfxAudioSource.PlayOneShot(lanternUnequip);
        }
    }
}