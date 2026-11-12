using UnityEngine;

public class furnitureManager : MonoBehaviour, IDataPersistence
{
    public FurnitureObject[] furnitureObjects;
    bool[] spawned = new bool[2];
    Vector3[] position = new Vector3[2];
    int[] rotation = new int[2];
    characterInventory invent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        invent = GameObject.FindGameObjectWithTag("Inventory").GetComponent<characterInventory>();
        for(int i = 0; i < furnitureObjects.Length; i++)
        {
            if (spawned[i])
            {
                GameObject obj = Instantiate(furnitureObjects[i].worldPrefab);
                obj.transform.position = position[i];
                for(int j = 0; j < rotation[i]; j++)
                {
                    obj.transform.Rotate(new Vector3(0, 0, 90));
                }
            }
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
    public void receivePosition(Vector3 pos)
    {
        int index = 0;
        while(furnitureObjects[index].itemName != invent.inventoryItemList[invent.selectedSlotNum].itemName)
        {
            index++;
        }
        spawned[index] = true;
        position[index] = pos;
    }
    public void LoadData(GameData data)
    {
        for(int i = 0; i < data.spawnedFurniture.Length; i++)
        {
            spawned[i] = data.spawnedFurniture[i];
        }
        for (int i = 0; i < data.spawnedFurniturePosition.Length; i++)
        {
            position[i] = data.spawnedFurniturePosition[i];
        }
        for (int i = 0; i < data.spawnedFurnitureRotation.Length; i++)
        {
            rotation[i] = data.spawnedFurnitureRotation[i];
        }
    }
    public void SaveData(ref GameData data)
    {
        if(data != null)
        {
            for (int i = 0; i < data.spawnedFurniture.Length; i++)
            {
                data.spawnedFurniture[i] = spawned[i];
            }
            for (int i = 0; i < data.spawnedFurniturePosition.Length; i++)
            {
                data.spawnedFurniturePosition[i] = position[i];
            }
            for (int i = 0; i < data.spawnedFurnitureRotation.Length; i++)
            {
                data.spawnedFurnitureRotation[i] = rotation[i];
            }
        }
    }
}
