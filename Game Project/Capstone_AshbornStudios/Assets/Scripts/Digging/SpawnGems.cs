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
    [Tooltip("Center point of each Biome. # OF ORIGINS MUST BE EQUAL TO # OF BIOMES")]
    public Vector3[] biomeOrigins = new Vector3[3];


    [Header("Gems")]
    public GameObject[,] gems = new GameObject[3,3];

    private void Start()
    {
        startGemSpawning(1);

    }

    public void startGemSpawning(int biomeIndex)
    {
        spawnNewGems(biomeIndex);
    }

    private void loadGemPositions()
    {
        //get spawn positions from save file and spawn them there
        return;
    }
    public void LoadData(GameData data)
    {

        for(int i = 0; i < gems.Length; i++)
        {
            for (int j = 0; j < gems.GetLength(i); j++)
            {
                gems[i,j].transform.position = data.gemPositions[i,j];
            }
        }
    }
    //save variables into game data
    public void SaveData(ref GameData data)
    {
        for (int i = 0; i < gems.Length; i++)
        {
            for (int j = 0; j < gems.GetLength(i); j++)
            {
                data.gemPositions[i, j] = gems[i, j].transform.position;
            }
        }
    }

    private void spawnNewGems(int biomeIndex)
    {
        //spawn gems
        for(int i = 0; i < gemsPerBiome + 1; i++)
        {
            Vector2 initialDirection = (Random.insideUnitCircle * biomeOrigins[biomeIndex]).normalized;

            Vector3 finalDirection = new Vector3(initialDirection.x, initialDirection.y, 0);

            float randomDistance = Random.Range(minSpawnRadius * 3, maxSpawnRadius * 3);

            float verticleOffset = Random.Range(-maxVerticleOffset, maxVerticleOffset);

            var point = biomeOrigins[biomeIndex] + finalDirection * randomDistance;

            Vector3 finalSpawnLocation = new Vector3(point.x, verticleOffset, point.y);

            GameObject newGem = gems[biomeIndex,i];

            

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
                    newGem = Instantiate(biomeOneGems[i]);
                    break;
            }

            newGem.transform.position = finalSpawnLocation;


        }
    }
}
