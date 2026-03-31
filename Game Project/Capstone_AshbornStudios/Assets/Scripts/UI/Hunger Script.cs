using UnityEngine;
using UnityEngine.UI;
public class HungerScript : MonoBehaviour
{
    [Header("Hunger")]
    public float maxHunger = 100f;
    public float currentHunger = 100f;

    [Header("Drain Rates")]
    public float idleDrain = 0.2f;
    public float walkDrain = 0.5f;
    public float runDrain = 1.5f;
    public float jumpDrain = 2f;
    public float digDrain = 1.5f;

    [Header("UI")]
    public Slider hungerBar;

    [Header("Low Hunger Movement Penalty")]
    public float lowHungerThreshold = 25f;
    public float reducedMoveSpeed = 2f;

    private float normalMoveSpeed;
    private bool speedReduced = false;

    private PlayerController playerController;

    void Start()
    {
        currentHunger = maxHunger;
        playerController = GetComponent<PlayerController>();

        if (playerController != null)
        {
            normalMoveSpeed = playerController.movementSpeed;
        }

        if (hungerBar != null)
        {
            hungerBar.maxValue = maxHunger;
            hungerBar.value = currentHunger;
        }
    }


    void Update()
    {
        DrainHunger();
        UpdateUI();
        HandleLowHungerSpeed();
        HandleJumpDrain();  
    }

    void DrainHunger()
    {
        float drain = GetDrainRate();
        currentHunger -= drain * Time.deltaTime;
        currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger);
    }

    float GetDrainRate()
    {
        if (playerController == null)
            return idleDrain;

        if (playerController.isRunning)
            return runDrain;

        if (playerController.isMoving)
            return walkDrain;

        return idleDrain;
    }

    void UpdateUI()
    {
        if (hungerBar == null) return;
        hungerBar.value = currentHunger;
    }

    void HandleLowHungerSpeed()
    {
        if (playerController == null) return;

        if (currentHunger <= lowHungerThreshold && !speedReduced)
        {
            playerController.movementSpeed = reducedMoveSpeed;
            speedReduced = true;
        }
        else if (currentHunger > lowHungerThreshold && speedReduced)
        {
            playerController.movementSpeed = normalMoveSpeed;
            speedReduced = false;
        }
    }
    void HandleJumpDrain()
    {
        if (playerController == null) return;

        if (playerController.isJumping)
        {
            currentHunger -= jumpDrain;
            currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger);
        }
    }
}
