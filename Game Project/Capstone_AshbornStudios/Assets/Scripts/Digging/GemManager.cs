using UnityEngine;

public class GemManager : MonoBehaviour, IDataPersistence
{
    public static GemManager instance { get; private set; }

    public static bool[] collectedGems = new bool[18];

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SaveData(ref GameData data)
    {
        data.collectedGems = collectedGems;
    }

    public void LoadData(GameData data)
    {
        collectedGems = data.collectedGems;
    }
}
