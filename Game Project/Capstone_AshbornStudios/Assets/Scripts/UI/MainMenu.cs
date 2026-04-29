using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour, IDataPersistence
{
    public int indexToLoad;
    [SerializeField] private Button newGame;
    [SerializeField] private Button contine;
    [SerializeField] private Button quit;
    private void Start()
    {
        if (!DataPersistenceManager.instance.hasData())
        {
            contine.interactable = false;
        }
    }
    public void Play()
    {
        disableButtons();
        SceneManager.LoadSceneAsync(indexToLoad);
    }
    public void newGameStart()
    {
        disableButtons();
        DataPersistenceManager.instance.NewGame();
        SceneManager.LoadSceneAsync(indexToLoad);
    }
    public void LoadData(GameData data)
    {
        this.indexToLoad = data.scenceIndex;
    }
    public void SaveData(ref GameData data)
    {
        return;
    }
    public void QuitGame()
    {
        disableButtons();
        Application.Quit();
    }
    private void disableButtons()
    {
        newGame.interactable = false;
        contine.interactable = false;
        quit.interactable = false;
    }
}
