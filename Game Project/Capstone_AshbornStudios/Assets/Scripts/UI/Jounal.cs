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
    public Texture[] gemThumbnails;
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

    //private variables
    int currentPage = 0;
    [HideInInspector]
    public bool isOpen = false;

    public void OpenJournal()
    {
        currentPage = 0;
        gameObject.SetActive(true);
        isOpen = true;
        UpdateText();
        UpdateShowButtons();
    }

    public void FlipPageLeft()
    {
        currentPage -= 1;
        UpdateText();
    }

    public void FlipPageRight()
    {
        currentPage += 1;
        UpdateText();
    }

    public void CloseJournal()
    {
        currentPage = 0;
        gameObject.SetActive(false);
        isOpen = false;
    }

    public void UpdateText()
    {

        int gemIndex = currentPage * 2;
        leftGemNameText.text = journalDatabase.getGemName(gemIndex);
        leftGemDescriptionText.text = journalDatabase.getGemDescription(gemIndex);
        leftGemDescriptionText.text = journalDatabase.getGemAbilityDescription(gemIndex);
        gemIndex += 1;
        if(!(gemIndex > numGems))
        {
            rightPage.SetActive(true);
            rightGemNameText.text = journalDatabase.getGemName(gemIndex);
            rightGemDescriptionText.text = journalDatabase.getGemDescription(gemIndex);
            rightGemDescriptionText.text = journalDatabase.getGemAbilityDescription(gemIndex);
        }
        else
        {
            rightPage.SetActive(false);
        }

    }

    public void UpdateShowButtons()
    {
        if(currentPage <= 0)
        {
            leftButton.enabled = false;
        }
        else
        {
            leftButton.enabled = true;
        }

        if(currentPage > numGems / 2)
        {
            rightButton.enabled = false;
        }
        else
        {
            rightButton.enabled = true;
        }
    }
}
