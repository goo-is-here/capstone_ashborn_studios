using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
public class MainMenu : MonoBehaviour, IDataPersistence
{
    public int indexToLoad;
    [SerializeField] private Button newGame;
    [SerializeField] private Button contine;
    [SerializeField] private Button quit;
    [SerializeField] AudioClip quitGame;
    [SerializeField] AudioClip playGame;
    AudioSource source;
    private void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        if (DataPersistenceManager.instance.hasData())
        {
            print("here");
            contine.interactable = true;
        }
    }
    public void Play()
    {
        disableButtons();
        source.PlayOneShot(playGame);
        StartCoroutine(waitForAudio(playGame.length, false));
    }
    public void newGameStart()
    {
        disableButtons();
        DataPersistenceManager.instance.NewGame();
        source.PlayOneShot(playGame);
        StartCoroutine(waitForAudio(playGame.length, false));
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
        source.PlayOneShot(quitGame);
        StartCoroutine(waitForAudio(quitGame.length, true));
    }
    private void disableButtons()
    {
        if(newGame != null) newGame.interactable = false;
        if(contine != null) contine.interactable = false;
        if(quit != null) quit.interactable = false;
    }
    IEnumerator waitForAudio(float length, bool quit)
    {
        yield return new WaitForSeconds(length);
        if (quit)
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadSceneAsync(indexToLoad);
        }
    }
}
