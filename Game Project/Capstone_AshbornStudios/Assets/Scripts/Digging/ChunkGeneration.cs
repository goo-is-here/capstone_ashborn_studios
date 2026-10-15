using UnityEngine;

public class ChunkGeneration : MonoBehaviour, IDataPersistence
{
    PlayerController cont;
    [SerializeField] int height;
    [SerializeField] int width;
    [SerializeField] GameObject chunkPrefab;
    GameObject[,,] chunks;
    bool[,,] generatedChunks;
    int[,,] chunkData;
    [SerializeField] float renderDistance;
    [SerializeField] float noise;
    [SerializeField] float heightThreshold;
    [SerializeField] float chunkSize;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        cont = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        chunks = new GameObject[width, height, width];
        generatedChunks = new bool[width, height, width];
        chunkData = new int[width, height, width];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int z = 0; z < width; z++)
                {
                    GameObject newChunk = Instantiate(chunkPrefab, new Vector3(transform.position.x + (x * chunkSize), transform.position.y - (y * chunkSize), transform.position.z + (z * chunkSize)), transform.rotation, transform);
                    chunks[x, y, z] = newChunk;
                    if (Vector3.Distance(newChunk.transform.position, cont.transform.position) > renderDistance)
                    {
                        generatedChunks[x, y, z] = false;
                        newChunk.SetActive(false);
                    }
                    else
                    {
                        generatedChunks[x, y, z] = true;
                        newChunk.GetComponent<DirtGeneration>().generateDirt(noise, heightThreshold);
                    }
                }
            }
        }
    }
    public void LoadData(GameData data)
    {
        if (data == null || data.chunkArray == null)
            return;

        foreach (chunkData dat in data.chunkArray)
        {
            int x = dat.chunkPositionx;
            int y = dat.chunkPositiony;
            int z = dat.chunkPositionz;
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int z = 0; z < width; z++)
                {
                    if (chunks[x, y, z] == null)
                        continue;

                    DirtGeneration dirt = chunks[x, y, z].GetComponent<DirtGeneration>();

                    if (dirt == null)
                        continue;

                    dirt.chunkBreak = chunkData[x, y, z] != 0 ? chunkData[x, y, z] : -1;
                }
            }
        }
    }

    public void SaveData(ref GameData data)
    {
        if (data == null || data.chunkArray == null)
            return;

        data.chunkArray.Clear();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int z = 0; z < width; z++)
                {
                    if (chunks[x, y, z] == null)
                        continue;

                    DirtGeneration dirt = chunks[x, y, z].GetComponent<DirtGeneration>();

                    if (dirt == null)
                        continue;

                    int breakData = dirt.chunkBreak;

                    chunkData newData = new chunkData(breakData, x, y, z);

                    data.chunkArray.Add(newData);
                }
            }
        }
        Debug.Log("Chunks saved in memory: " + data.chunkArray.Count);
        Debug.Log(JsonUtility.ToJson(data, true));
    }
    // Update is called once per frame
    void Update()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int z = 0; z < width; z++)
                {
                    if (Vector3.Distance(chunks[x, y, z].transform.position, cont.transform.position) < renderDistance)
                    {
                        chunks[x, y, z].SetActive(true);
                        if (!generatedChunks[x, y, z])
                        {
                            chunks[x, y, z].GetComponent<DirtGeneration>().generateDirt(noise, heightThreshold);
                            generatedChunks[x, y, z] = true;
                        }
                    }
                    else
                    {
                        chunks[x, y, z].SetActive(false);
                    }

                }
            }
        }
    }
}
