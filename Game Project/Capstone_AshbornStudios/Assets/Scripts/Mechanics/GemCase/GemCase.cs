using UnityEngine;

public class GemCase : MonoBehaviour, IDataPersistence
{
    public GameObject[] gems;

    public void SaveData(ref GameData data)
    {

    }

    public void LoadData(GameData data)
    {
        for(int i = 0; i < gems.Length; i++)
        {
            if (data.collectedGems[i] == true)
            {
                gems[i].SetActive(true);
            }
            else
            {
                gems[i].SetActive(false);
            }
        }
    }


}
