using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class SpawnGems : MonoBehaviour, IDataPersistence
{

    [Header("Spawn Settigns")]
    [Tooltip("Units in blocks")]
    public float minSpawnRadius;
    [Tooltip("Units in blocks")]
    public float maxSpawnRadius;
    public float maxVerticleOffset;
    [Tooltip("Center point of each Biome. # OF ORIGINS MUST BE EQUAL TO # OF BIOMES")]
    public Vector3[] biomeOrigins = new Vector3[6];

    
    //all the reference arrays to plug stuff in
    [Header("Gems")]
    public GameObject[] biomeOneGems = new GameObject[3];
    public GameObject[] biomeTwoGems = new GameObject[3];
    public GameObject[] biomeThreeGems = new GameObject[3];
    public GameObject[] biomeFourGems = new GameObject[3];
    public GameObject[] biomeFiveGems = new GameObject[3];
    public GameObject[] biomeSixGems = new GameObject[3];

    //the above arrays get added to the refrences array to centralize everything

    public GameObject[,] gemReferences = new GameObject[3,3];

    //the spawned gems stores refrences to the gems that have already been spawned into the world
    public GameObject[,] spawnedGems = new GameObject[3,3];

    //purely used for saving and loading idk why but it only works but using a sperate array to store the positions for save and load
    private Vector3[,] gemPositions = new Vector3[3, 3];

    //for debugging
    public bool forceSpawnGems = false;

    void Start()
    {
        if (forceSpawnGems)
        {
            InitializeArrays();
            spawnNewGems(1);
            spawnNewGems(2);
            spawnNewGems(3);
        }
    }

    //loads gems positions and created gems
    public void LoadData(GameData data)
    {
        InitializeArrays();
        spawnNewGems(1);
        spawnNewGems(2);
        spawnNewGems(3);
        int index = 0;

        for(int i = 0; i < gemReferences.GetLength(0); i++)
        {
            for (int j = 0; j < gemReferences.GetLength(1); j++)
            {
                
                if (!data.collectedGems[i * 3 + j])
                {
                    if (data.gemPositions[i * 3 + j] != Vector3.zero)
                    {
                        spawnedGems[i, j].transform.position = data.gemPositions[i * 3 + j];
                    }
                }
                else
                {
                    StartCoroutine(SetGemAsCollectedAfterLoad(spawnedGems[i, j].GetComponent<gem>()));
                }
            }
        }
    }

    private IEnumerator SetGemAsCollectedAfterLoad(gem gem)
    {
        yield return null;
        yield return null;
        gem.Collect();
    }

    //save gems positions
    public void SaveData(ref GameData data)
    {
        var index = 0;
        for (int i = 0; i < gemReferences.GetLength(0); i++)
        {
            for (int j = 0; j < gemReferences.GetLength(1); j++)
            {
                print(spawnedGems[i,j]);
                if (!GemManager.collectedGems[i * 3 + j]) data.gemPositions[i * 3 + j] = gemPositions[i,j];
                index += 1;
            }
        }
    }

    public void InitializeArrays()
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
                    case 3:
                        gemReferences[i, j] = biomeFourGems[j];
                        break;
                    case 4:
                        gemReferences[i, j] = biomeFiveGems[j];
                        break;
                    case 5:
                        gemReferences[i, j] = biomeSixGems[j];
                        break;
                }
            }
        }
    }

    //spawns in the gems
    private void spawnNewGems(int biomeIndex)
    {
        for (int i = 0; i < gemReferences.GetLength(1); i++)
        {
           
            Vector2 initialDirection = (Random.insideUnitCircle).normalized;

            Vector3 finalDirection = new Vector3(initialDirection.x, 0, initialDirection.y);

            float randomDistance = Random.Range(minSpawnRadius, maxSpawnRadius);

            float verticleOffset = Random.Range(-maxVerticleOffset, maxVerticleOffset);

            var point = biomeOrigins[biomeIndex - 1] + (finalDirection * randomDistance);

            Vector3 finalSpawnLocation = new Vector3(point.x, point.y + verticleOffset, point.z);

            GameObject newGem = Instantiate(gemReferences[biomeIndex - 1,i]);

            newGem.transform.position = finalSpawnLocation;
            spawnedGems[biomeIndex - 1, i] = newGem;

            //set the reference variables for the gem script
            //Gem ID is just x and y location of the gem in the 2d array here in the gem spawner. Used for referencing it in the collected gems 2d array.
            newGem.GetComponent<gem>().gemID = (biomeIndex - 1) * 3 + i;
            newGem.GetComponent<gem>().gemSpawner = this.gameObject;

            gemPositions[biomeIndex - 1, i] = newGem.transform.position;

            Debug.Log(spawnedGems[biomeIndex - 1, i]);
        }

    }

    //called from the gem script when a gem is picked up
    public void setGemAsCollected(int gemID)
    {
        Debug.Log(gemID);
        GemManager.collectedGems[gemID] = true;
    }
}
