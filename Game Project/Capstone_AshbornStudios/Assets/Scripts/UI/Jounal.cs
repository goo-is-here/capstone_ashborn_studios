using Journal;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Jounal : MonoBehaviour
{
    [Header("Journal Parameters")]
    public int numPages;
    public int numGems;

    [Header("Refrences")]
    public GameObject leftPage;
    public GameObject rightPage;
    public Sprite[] gemThumbnails;
    public Image leftImage;
    public Image rightImage;
    public TextMeshProUGUI rightGemNameText;
    public TextMeshProUGUI leftGemNameText;
    public TextMeshProUGUI leftGemDescriptionText;
    public TextMeshProUGUI rightGemDescriptionText;
    public TextMeshProUGUI leftGemAbilityText;
    public TextMeshProUGUI rightGemAbilityText;
    public JournalDatabase journalDatabase;
    public Button leftButton;
    public Button rightButton;
    public PlayerController playerController;

    //private variables
    int currentPage = 0;
    [HideInInspector]
    public bool isOpen = false;
    private Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    public void OpenJournal()
    {
        currentPage = 0;
        canvas.enabled = true;
        isOpen = true;
        UpdateText();
        UpdateShowButtons();
        Cursor.lockState = CursorLockMode.None;
    }

    public void FlipPageLeft()
    {
        currentPage -= 1;
        UpdateText();
        UpdateShowButtons();
    }

    public void FlipPageRight()
    {
        currentPage += 1;
        UpdateText();
        UpdateShowButtons();
    }

    public void CloseJournal()
    {
        currentPage = 0;
        canvas.enabled = false;
        isOpen = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UpdateText()
    {
        
        int gemIndex = (currentPage * 2) + 1;
        if (playerController.collectedGems[gemIndex - 1])
        {
            leftPage.SetActive(true);
            leftGemNameText.text = journalDatabase.getGemName(gemIndex);
            leftGemDescriptionText.text = journalDatabase.getGemDescription(gemIndex);
            leftGemAbilityText.text = journalDatabase.getGemAbilityDescription(gemIndex);
            //leftImage.sprite = gemThumbnails[gemIndex - 1];
        }
        else
        {
            leftPage.SetActive(false);
        }
        gemIndex += 1;
        if(!(gemIndex > numGems))
        {
            if (playerController.collectedGems[gemIndex - 1])
            {
                rightPage.SetActive(true);
                rightGemNameText.text = journalDatabase.getGemName(gemIndex);
                rightGemDescriptionText.text = journalDatabase.getGemDescription(gemIndex);
                rightGemAbilityText.text = journalDatabase.getGemAbilityDescription(gemIndex);
                //rightImage.sprite = gemThumbnails[gemIndex - 1];
            }
            else
            {
                rightPage.SetActive(false);
            }
        }
        else
        {
            rightPage.SetActive(false);
        }

    }

    public void UpdateShowButtons()
    {
        if(currentPage == 0)
        {
            leftButton.gameObject.SetActive(false);
        }
        else
        {
            leftButton.gameObject.SetActive(true);
        }

        if(currentPage >= numGems / 2)
        {
            rightButton.gameObject.SetActive(false);
        }
        else
        {
            rightButton.gameObject.SetActive(true);
        }
    }
}
