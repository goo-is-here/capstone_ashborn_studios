using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public GameObject winScreenPanel;
    private bool hasActivated = false;

    private void Start()
    {
        if (winScreenPanel != null)
            winScreenPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Win trigger entered by: " + other.name);

        if (hasActivated)
        {
            Debug.Log("Win screen already activated.");
            return;
        }

        Debug.Log("Root object tag: " + other.transform.root.tag);

        if (!other.transform.root.CompareTag("Player"))
        {
            Debug.Log("Entered object is not the player.");
            return;
        }

        if (Treasure_Manager.Instance == null)
        {
            Debug.LogWarning("Treasure_Manager.Instance is NULL.");
            return;
        }

        Debug.Log("Collected IDs right now: " + Treasure_Manager.Instance.collectedTreasureIDs.Count);
        Debug.Log("HasAllTreasures() returns: " + Treasure_Manager.Instance.HasAllTreasures());

        if (Treasure_Manager.Instance.HasAllTreasures())
        {
            ActivateWinScreen();
        }
        else
        {
            Debug.Log("Not all treasures collected yet.");
        }
    }

    public void ActivateWinScreen()
    {
        hasActivated = true;

        if (winScreenPanel == null)
        {
            Debug.LogWarning("winScreenPanel is not assigned.");
            return;
        }

        Debug.Log("Activating win screen now.");
        winScreenPanel.SetActive(true);

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ContinueGame()
    {
        if (winScreenPanel != null)
            winScreenPanel.SetActive(false);

        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

