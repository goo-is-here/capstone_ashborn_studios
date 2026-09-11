using Mono.Cecil.Cil;
using NUnit.Framework;
using UnityEngine;
using static UnityEngine.UI.Image;

public class SpawnGems : MonoBehaviour
{
    [Header("Spawn Params")]
    public int gemsPerBiome = 3;
    [Tooltip("Units in blocks")]
    public float minSpawnRadius;
    [Tooltip("Units in blocks")]
    public float maxSpawnRadius;
    public float maxVerticleOffset;
    public Vector3 origin = Vector3.zero;

    [Header("Gems")]
    public GameObject[] biomeOneGems = new GameObject[3];
    public GameObject[] biomeTwoGems = new GameObject[3];
    public GameObject[] biomeThreeGems = new GameObject[3];

    private void Start()
    {
        origin = transform.position;
        spawnNewGems(1);

    }

    public void startGemSpawning(int biomeIndex)
    {
        bool gemsHaveSaveData = false;

        //check save file to see if gems have spawn positions
        if(gemsHaveSaveData) //not gems have spawn positions// 
        {
            spawnNewGems(biomeIndex);
        }
        else
        {
            loadGemPositions();
        }
    }

    private void loadGemPositions()
    {
        //get spawn positions from save file and spawn them there
        return;
    }
    public void LoadData(GameData data)
    {
        for(int i = 0; i < biomeOneGems.Length; i++)
        {
            biomeOneGems[i].transform.position = data.gemPositions[0, i];
        }
        for (int i = 0; i < biomeTwoGems.Length; i++)
        {
            biomeTwoGems[i].transform.position = data.gemPositions[1, i];
        }
        for (int i = 0; i < biomeThreeGems.Length; i++)
        {
            biomeThreeGems[i].transform.position = data.gemPositions[2, i];
        }
    }
    //save variables into game data
    public void SaveData(ref GameData data)
    {
        if(data != null)
        {
            for (int i = 0; i < biomeOneGems.Length; i++)
            {
                data.gemPositions[0, i] = biomeOneGems[i].transform.position;
            }
            for (int i = 0; i < biomeTwoGems.Length; i++)
            {
                data.gemPositions[1, i] = biomeTwoGems[i].transform.position;
            }
            for (int i = 0; i < biomeThreeGems.Length; i++)
            {
                data.gemPositions[2, i] = biomeThreeGems[i].transform.position;
            }
        }

    }
    private void spawnNewGems(int biomeIndex)
    {
        //spawn gems
        for(int i = 0; i < gemsPerBiome + 1; i++)
        {
            Vector2 initialDirection = (Random.insideUnitCircle * origin).normalized;

            Vector3 finalDirection = new Vector3(initialDirection.x, initialDirection.y, 0);

            float randomDistance = Random.Range(minSpawnRadius * 3, maxSpawnRadius * 3);

            float verticleOffset = Random.Range(-maxVerticleOffset, maxVerticleOffset);

            var point = origin + finalDirection * randomDistance;

            Vector3 finalSpawnLocation = new Vector3(point.x, verticleOffset, point.y);

            GameObject newGem = null;

            switch (biomeIndex)
            {
                case 1:
                    newGem = Instantiate(biomeOneGems[i]);
                    break;
                case 2:
                    newGem = Instantiate(biomeTwoGems[i]);
                    break;
                case 3:
                    newGem = Instantiate(biomeThreeGems[i]);
                    break;
                default:
                    Debug.Log("Don't forget to keep biome index between 1 and 3");
                    break;
            }

            newGem.transform.position = finalSpawnLocation;


        }
    }
}
