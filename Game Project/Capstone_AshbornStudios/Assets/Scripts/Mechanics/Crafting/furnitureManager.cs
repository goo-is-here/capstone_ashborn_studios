using UnityEngine;

public class furnitureManager : MonoBehaviour, IDataPersistence
{
    public FurnitureObject[] furnitureObjects;
    bool[] spawned;
    Vector3[] position;
    int[] rotation;
    characterInventory invent;
    GameObject[] spawnedObjects;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        invent = GameObject.FindGameObjectWithTag("Inventory").GetComponent<characterInventory>();
        for(int i = 0; i < furnitureObjects.Length; i++)
        {
            spawnedObjects[i].transform.Rotate(0, 0, 90 * rotation[i]);
        }
    }
    public void receiveRotate(int rotate)
    {
        for(int i = 0; i < furnitureObjects.Length; i++)
        {
            if(furnitureObjects[i].itemName == invent.inventoryItemList[invent.selectedSlotNum].itemName)
            {
                rotation[i] = rotate;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadData(GameData data)
    {
        
    }
    public void SaveData(ref GameData data)
    {

    }
}
