using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DirtGeneration : MonoBehaviour
{
    public int size;
    [SerializeField] GameObject dirtPrefab;
    GameObject[,,] dirtParticles;
    GameObject[,,] oreBits;
    [HideInInspector] public float dirtSize;
    ChunkGeneration chunk;
    [HideInInspector] public bool[] chunkBreak;

    [Range(1,100)]
    [SerializeField] List<float> rarityTiers;
    [SerializeField] List<GameObject> objectToSpawn;
    [SerializeField] int oreLimit;
    int numOre = 0;
    public int seed;
    [SerializeField] ClutterObject[] clutter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        dirtSize = dirtPrefab.GetComponent<MeshRenderer>().bounds.size.x;
        dirtParticles = new GameObject[size, size, size];
        oreBits = new GameObject[size, size, size];
        chunkBreak = new bool[size * size * size];
    }
    public void generateDirt(float noise, float heightThreshold)
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                for (int k = 0; k < size; k++)
                {
                    Vector3 dirtPos = new Vector3(transform.position.x + (i * dirtSize), transform.position.y + (j * dirtSize), transform.position.z + (k * dirtSize));
                    if (perlinNoise(noise, dirtPos.x, dirtPos.y, dirtPos.z) > heightThreshold)
                    {
                        GameObject newDirt = Instantiate(dirtPrefab, dirtPos, transform.rotation, transform);
                        newDirt.GetComponent<dirt>().setThis(i, j, k);
                        dirtParticles[i, j, k] = newDirt;
                        int randChance = Random.Range(0, 100);
                        if (randChance > 90)
                        {
                            oreBits[i, j, k] = oreCheck(newDirt.transform.position);
                        }
                    }
                }
            }
        }
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                for (int k = 0; k < size; k++)
                {
                    int randChance = Random.Range(0, 100);
                    if (randChance > 1)
                    {
                        if(dirtParticles[i, j, k] != null)
                        {
                            addClutter(i, j, k, dirtParticles[i, j, k].transform.position);
                        }
                    }
                }
            }
        }

    }
    void addClutter(int x, int y, int z, Vector3 pos)
    {
        int clutterIndex = Random.Range(0, clutter.Length);
        ClutterObject selected = clutter[clutterIndex];
        bool open = true;
        for(int i = 0; i < selected.length; i++)
        {
            for (int j = 0; j < selected.width; j++)
            {
                if(x+i>= size || z + j >= size)
                {
                    return;
                }
                if(dirtParticles[x + i, y, z + j] != null)
                {
                    dirt dt = dirtParticles[x + i, y, z + j].GetComponent<dirt>();
                    if (dt != null && !dt.open)
                    {
                        open = false;
                    }
                }
            }
        }
        if (!open)
        {
            return;
        }
        bool clear = true;
        for (int i = 0; i < selected.length; i++)
        {
            for (int j = 0; j < selected.width; j++)
            {
                for (int k = 1; k <= selected.height; k++)
                {
                    if(y+k >= size)
                    {
                        return;
                    }
                    if(dirtParticles[x+i, k+y, j+z] != null)
                    {
                        clear = false;
                    }
                }
            }
        }
        if (clear)
        {
            Instantiate(selected.obj, pos, transform.rotation, dirtParticles[x, y, z].transform);
            for (int i = 0; i < selected.length; i++)
            {
                for (int j = 0; j < selected.width; j++)
                {
                    dirt dt = dirtParticles[x + i, y, z + j].GetComponent<dirt>();
                    if (dt != null && open)
                    {
                        dt.open = false;
                    }
                }
            }
        }
    }
    public void breakBlocks()
    {
        if (chunkBreak != null)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    for (int k = 0; k < size; k++)
                    {
                        if (chunkBreak[i * size * size + j * size + k])
                        {
                            Destroy(dirtParticles[i, j, k]);
                            Destroy(oreBits[i, j, k]);
                        }
                        else
                        {
                            Vector3 dirtPos = new Vector3(transform.position.x + (i * dirtSize), transform.position.y + (j * dirtSize), transform.position.z + (k * dirtSize));
                        }
                    }
                }
            }
        }
    }
    private GameObject oreCheck(Vector3 pos)
    {
        if(numOre < oreLimit)
        {
            bool spawned = false;
            foreach (GameObject obj in objectToSpawn)
            {
                int i = 0;
                float random = Random.Range(1f, 100f);
                if (i < rarityTiers.Count && random > rarityTiers[i] && !spawned)
                {
                    numOre++;
                    GameObject ore = Instantiate(obj, pos, transform.rotation, transform);
                    return ore;

                }
                i++;
            }
            
        }
        return null;
    }
    public void receiveBreak(int x, int y, int z)
    {
        chunkBreak[x * size * size + y * size + z] = true;
    }
    float perlinNoise(float noise, float x, float y, float z)
    {
        x = (float)x * noise;
        y = (float)y * noise;
        z = (float)z * noise;
        float xy = Mathf.PerlinNoise(x, y);
        float yz = Mathf.PerlinNoise(y, z);
        float zx = Mathf.PerlinNoise(z, x);
        float yx = Mathf.PerlinNoise(y, x);
        float zy = Mathf.PerlinNoise(z, y);
        float xz = Mathf.PerlinNoise(x, z);
        float average = (xy + yz + zx + yx + zy + xz) / 6f;
        return average;
    }
}
