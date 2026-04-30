using UnityEngine;

public class ruinsTeleport : MonoBehaviour, IDataPersistence
{
    int loc;
    [SerializeField]GameObject[] spawnLocations;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(loc < 0)
        {
            loc = Random.Range(0, spawnLocations.Length);
        }
        transform.position = spawnLocations[loc].transform.position;
    }
    public void LoadData(GameData data)
    {
        loc = data.ruinsLocation;
    }
    public void SaveData(ref GameData data)
    {
        data.ruinsLocation = loc;
    }
}
