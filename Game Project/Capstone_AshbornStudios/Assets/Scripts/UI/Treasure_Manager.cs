using System.Collections.Generic;
using UnityEngine;

public class Treasure_Manager : MonoBehaviour
{
    public static Treasure_Manager Instance;

    [Header("Treasure Storage")]
    public List<Treasure_Item> collectedTreasures = new List<Treasure_Item>();

    [Header("Treasure State Variable")]
    public List<string> collectedTreasureIDs = new List<string>();

    public int totalTreasuresInGame = 5; // set this manually for now

    public bool HasAllTreasures()
    {
        return collectedTreasureIDs.Count >= totalTreasuresInGame;
    }

    public GameObject winScreenPanel;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        winScreenPanel.SetActive(false);
    }

    public void AddTreasure(Treasure_Item newTreasure)
    {
        if (!collectedTreasureIDs.Contains(newTreasure.treasureID))
        {
            collectedTreasureIDs.Add(newTreasure.treasureID);
            collectedTreasures.Add(newTreasure);

            Debug.Log("Collected treasure: " + newTreasure.treasureName);
        }
        else
        {
            Debug.Log("Treasure already collected: " + newTreasure.treasureName);
        }
    }

    public bool HasTreasure(string treasureID)
    {
        return collectedTreasureIDs.Contains(treasureID);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (Treasure_Manager.Instance == null) return;

        if (Treasure_Manager.Instance.HasAllTreasures())
        {
            ShowWinPrompt();
        }
        else
        {
            Debug.Log("Player entered trophy room but not all treasures collected.");
        }
    }
    void ShowWinPrompt()
    {
        if (winScreenPanel != null)
            winScreenPanel.SetActive(true);

        // Pause game
        Time.timeScale = 0f;

        // Unlock cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("Win condition reached!");
    }
}


