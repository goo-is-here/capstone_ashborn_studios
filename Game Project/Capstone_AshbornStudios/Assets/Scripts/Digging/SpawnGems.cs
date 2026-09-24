using Mono.Cecil.Cil;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class SpawnGems : MonoBehaviour, IDataPersistence
{
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
    public GameObject[,] gemReferences = new GameObject[3,3];
    public GameObject[,] spawnedGems = new GameObject[3,3];

    private bool[,] collectedGems = new bool[3,3];

    //loads gems positions and created gems
    public void LoadData(GameData data)
    {
        spawnNewGems(1);
        spawnNewGems(2);
        spawnNewGems(3);

        collectedGems = data.collectedGems;

        for(int i = 0; i < gemReferences.GetLength(0); i++)
        {
            for (int j = 0; j < gemReferences.GetLength(1) - 1; j++)
            {
                if (!collectedGems[i, j])
                {
                    if (data.gemPositions[i, j] != Vector3.zero)
                    {
                        spawnedGems[i, j].transform.position = data.gemPositions[i, j];
                        Debug.Log("Changing local");
                    }
                }
                else
                {
                    Destroy(spawnedGems[i, j].gameObject);
                    Debug.Log("Killing");
                }
            }
        }
        Debug.Log("Ran");
    }

    //save gems positions
    public void SaveData(ref GameData data)
    {
        data.collectedGems = collectedGems;
        for (int i = 0; i < gemReferences.GetLength(0); i++)
        {
            for (int j = 0; j < gemReferences.GetLength(1); j++)
            {

                data.gemPositions[i, j] = spawnedGems[i, j].transform.position;

            }
        }
    }

    //spawns in the gems
    private void spawnNewGems(int biomeIndex)
    {

        for (int i = 0; i < gemReferences.GetLength(0); i++)
        {
            for (int j = 0; j < gemReferences.GetLength(1); j++)
            {
                switch (i)
                {
                    case 0:
                        gemReferences[i, j] = biomeOneGems[j];
                        break;
                    case 1:
                        gemReferences[i, j] = biomeTwoGems[j];
                        break;
                    case 2:
                        gemReferences[i, j] = biomeThreeGems[j];
                        break;
                }

                collectedGems[i, j] = false;
            }
        }

        for (int i = 0; i < gemReferences.GetLength(1); i++)
        {
           
            Vector2 initialDirection = (Random.insideUnitCircle).normalized;

            Vector3 finalDirection = new Vector3(initialDirection.x, 0, initialDirection.y);

            float randomDistance = Random.Range(minSpawnRadius * 3, maxSpawnRadius * 3);

            float verticleOffset = Random.Range(-maxVerticleOffset, maxVerticleOffset);

            var point = biomeOrigins[biomeIndex - 1] + finalDirection * randomDistance;

            Vector3 finalSpawnLocation = new Vector3(point.x, verticleOffset, point.z);

            GameObject newGem = Instantiate(gemReferences[biomeIndex - 1,i]);

            newGem.transform.position = finalSpawnLocation;
            spawnedGems[biomeIndex - 1, i] = newGem;

            print(spawnedGems[biomeIndex - 1, i]);

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
