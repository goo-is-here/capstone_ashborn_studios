using Mono.Cecil.Cil;
using NUnit.Framework;
using Unity.VisualScripting;
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
    public GameObject[] biomeOneGems = new GameObject[3];
    public GameObject[] biomeTwoGems = new GameObject[3];
    public GameObject[] biomeThreeGems = new GameObject[3];

    //the above arrays get added to the gems array
    public GameObject[,] gems = new GameObject[3,3];
    private bool[,] collectedGems = new bool[3,3];
    
    private void Start()
    {
        for (int i = 0; i < gems.Length; i++)
        {
            for (int j = 0; j < gems.GetLength(i); j++)
            {
                switch (i)
                {
                    case 1:
                        gems[i, j] = biomeOneGems[j];
                        break;
                    case 2:
                        gems[i, j] = biomeTwoGems[j];
                        break;
                    case 3:
                        gems[i, j] = biomeThreeGems[j];
                        break;
                }
            }
        }
        spawnNewGems(1);
    }

    //loads gems positions and created gems
    public void LoadData(GameData data)
    {

        collectedGems = data.collectedGems;

        for(int i = 0; i < gems.Length; i++)
        {
            for (int j = 0; j < gems.GetLength(i); j++)
            {
                if (!collectedGems[i, j])
                {
                    gems[i, j].transform.position = data.gemPositions[i, j];
                }
                else
                {
                    Destroy(gems[i, j].gameObject);
                }
            }
        }
    }

    //save gems positions
    public void SaveData(ref GameData data)
    {
        data.collectedGems = collectedGems;
        for (int i = 0; i < gems.Length; i++)
        {
            for (int j = 0; j < gems.GetLength(i); j++)
            {
                data.gemPositions[i, j] = gems[i, j].transform.position;

            }
        }
    }

    //spawns in the gems
    private void spawnNewGems(int biomeIndex)
    {
        for(int i = 0; i < gemsPerBiome + 1; i++)
        {
            Vector2 initialDirection = (Random.insideUnitCircle * biomeOrigins[biomeIndex]).normalized;

            Vector3 finalDirection = new Vector3(initialDirection.x, initialDirection.y, 0);

            float randomDistance = Random.Range(minSpawnRadius * 3, maxSpawnRadius * 3);

            float verticleOffset = Random.Range(-maxVerticleOffset, maxVerticleOffset);

            var point = biomeOrigins[biomeIndex] + finalDirection * randomDistance;

            Vector3 finalSpawnLocation = new Vector3(point.x, verticleOffset, point.y);

            GameObject newGem = gems[biomeIndex,i];

            newGem.transform.position = finalSpawnLocation;

            //set the reference variables for the gem script
            //Gem ID is just x and y location of the gem in the 2d array here in the gem spawner. Used for referencing it in the collected gems 2d array.
            newGem.GetComponent<gem>().gemID = biomeIndex * 10 + i;
            newGem.GetComponent<gem>().gemSpawner = this.gameObject;



        }
    }

    //called from the gem script when a gem is picked up
    public void setGemAsCollected(int gemID)
    {
        int horizontalCoord = gemID / 10;
        int verticalCoord = gemID % 10;

        collectedGems[horizontalCoord, verticalCoord] = true;
    }
}
