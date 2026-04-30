using UnityEngine;

public class growRoomEnter : MonoBehaviour, IDataPersistence
{
    public bool entered = false;
    GameObject[] plants;
    [SerializeField] diggableBlock[] rubble;
    private void Start()
    {
        plants = GameObject.FindGameObjectsWithTag("plant");
        
    }
    public void LoadData(GameData data)
    {
        entered = data.growRoomEntered;
    }
    public void SaveData(ref GameData data)
    {
        data.growRoomEntered = entered;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !entered)
        {
            
            entered = true;
            for(int i = 0; i < plants.Length; i++)
            {
                plants[i].GetComponent<growPlant>().resetScale();
            }
        }
    }
}
