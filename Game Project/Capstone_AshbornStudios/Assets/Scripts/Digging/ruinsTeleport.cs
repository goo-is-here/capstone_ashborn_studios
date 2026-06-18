using UnityEngine;

public class ruinsTeleport : MonoBehaviour, IDataPersistence
{
    //list of the possible different ruin locations
    int loc;
    [SerializeField]GameObject[] spawnLocations;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //picks a random number from the spawn array
        if(loc < 0)
        {
            loc = Random.Range(0, spawnLocations.Length);
        }
        //teleports it
        transform.position = spawnLocations[loc].transform.position;
    }
    //loads it into the same location every time
    public void LoadData(GameData data)
    {
        loc = data.ruinsLocation;
    }
    //saves the location to load
    public void SaveData(ref GameData data)
    {
        data.ruinsLocation = loc;
    }
}
