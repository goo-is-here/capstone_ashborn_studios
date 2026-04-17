using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public GameObject winScreenPanel;

    public void Continue()
    {
        if (winScreenPanel != null)
            winScreenPanel.SetActive(false);

       
        Time.timeScale = 1f;

        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Debug.Log("Continuing game...");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}

