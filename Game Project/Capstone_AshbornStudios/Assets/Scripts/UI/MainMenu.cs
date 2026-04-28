using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour, IDataPersistence
{
    public int indexToLoad;
    public void Play()
    {
        SceneManager.LoadScene(indexToLoad);
    }
    public void LoadData(GameData data)
    {
        print("loading " + data.scenceIndex);
        this.indexToLoad = data.scenceIndex;
    }
    public void SaveData(ref GameData data)
    {
        return;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
