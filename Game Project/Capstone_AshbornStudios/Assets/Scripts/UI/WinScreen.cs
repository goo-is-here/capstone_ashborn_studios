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
        Debug.Log("Entered win trigger: " + other.name);

        if (hasActivated) return;

        if (!other.transform.root.CompareTag("Player"))
        {
            Debug.Log("Not the player.");
            return;
        }

        if (Treasure_Manager.Instance == null)
        {
            Debug.LogWarning("Treasure_Manager not found.");
            return;
        }

        Debug.Log("HasAllTreasures result: " + Treasure_Manager.Instance.HasAllTreasures());

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
            Debug.LogWarning("Win screen panel is not assigned.");
            return;
        }

        winScreenPanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("Win screen activated.");
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

