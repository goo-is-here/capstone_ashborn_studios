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
    public Texture[] gemThumbnails;
    public Image leftImage;
    public Image rightImage;
    public TextMeshPro rightGemNameText;
    public TextMeshPro leftGemNameText;
    public TextMeshPro leftGemDescriptionText;
    public TextMeshPro rightGemDescriptionText;
    public TextMeshPro leftGemAbilityText;
    public TextMeshPro rightGemAbilityText;
    public JournalDatabase journalDatabase;

    //private variables
    int currentPage = 0;

    public void OpenJournal()
    {
        currentPage = 0;
        gameObject.SetActive(true);
        UpdateText();
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
    }

    public void UpdateText()
    {

        int gemIndex = currentPage * 2;
        leftGemNameText.text = journalDatabase.getGemName(gemIndex);
        leftGemDescriptionText.text = journalDatabase.getGemDescription(gemIndex);
        leftGemDescriptionText.text = journalDatabase.getGemAbilityDescription(gemIndex);
    }
}
