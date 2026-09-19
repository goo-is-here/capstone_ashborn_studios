using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [Header("Player Controller Script")]
    public PlayerController controller;
    [Header("Journal Reference")]
    public Jounal journal;
    private InputAction moveAction, lookAction, jumpAction, digAction, journalAction;
    Vector2 movementVector;
    // Gets the actions to look for
    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");
        jumpAction = InputSystem.actions.FindAction("Jump");
        digAction = InputSystem.actions.FindAction("Attack");
        journalAction = InputSystem.actions.FindAction("OpenJournal");
    }
    void Start()
    {
        //assigns finds player object. It should ALWAYS be tagged with player. It should also be the only object tagged this way
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Checks edge cases or if in menu
        if(controller == null || !controller.canMove)
        {
            return;
        }
        
        if (controller == null)
        {
            print("fuck");
        }
        else
        {
            movementVector = moveAction.ReadValue<Vector2>();
            controller.Move(movementVector);
        }
        

        Vector2 lookVector = lookAction.ReadValue<Vector2>();
        controller.Rotate(lookVector);

    }
    //prevents the errors when changing scenes. Don't remove
    private void OnEnable()
    {
        digAction.performed += OnDigPerformed;
        jumpAction.performed += OnJumpPerformed;
        journalAction.performed += OnOpenJournalPerformed;

    }
    private void OnDisable()
    {
        digAction.performed -= OnDigPerformed;
        jumpAction.performed -= OnJumpPerformed;
        journalAction.performed -= OnOpenJournalPerformed;
    }
    public float getForward()
    {
        return movementVector.y;
    }
    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        if(controller.canMove) controller.Jump();

    }
    private void OnDigPerformed(InputAction.CallbackContext context)
    {
        if(controller.canMove) controller.Dig();
    }

    private void OnOpenJournalPerformed(InputAction.CallbackContext context)
    {
        if (!journal.isOpen)
        {
            journal.OpenJournal();
            controller.canLook = false;
        }
        else
        {
            journal.CloseJournal();
            controller.canLook = true;
        }
    }
}
