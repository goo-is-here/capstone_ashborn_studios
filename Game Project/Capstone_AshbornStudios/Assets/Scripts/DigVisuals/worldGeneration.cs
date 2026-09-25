using UnityEngine;

public class worldGeneration : MonoBehaviour
{
   
    private int width = 10;
    private int height = 10;
    [SerializeField] private int biomeOneSize = 10;
    [SerializeField] private int biomeOneHeight = 4;
    [SerializeField] private GameObject chunkPrefab;
    [SerializeField] private Transform player;
    [SerializeField] private int renderRange;
    private GameObject[,,] chunks;
    [SerializeField] GameObject wall;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        width = chunkPrefab.GetComponent<MarchingCubes>().width;
        height = chunkPrefab.GetComponent<MarchingCubes>().height;
        chunks = new GameObject[biomeOneHeight, biomeOneSize, biomeOneSize];
        for (int i = 0; i < biomeOneHeight; i++)
        {
            for (int j = 0; j < biomeOneSize; j++)
            {
                for (int k = 0; k < biomeOneSize; k++)
                {
                    Vector3 position = new Vector3(j * width, -i * height, k * width);
                    Vector3 offset = new Vector3(biomeOneSize / 2, 0, 10.5f);
                    chunks[i, j, k] = Instantiate(chunkPrefab, transform);
                    chunks[i, j, k].transform.position = position + offset;
                    chunks[i, j, k].SetActive(false);
                }
            }
        }
        transform.position += (Vector3.left * (float)biomeOneSize/2 * (float)width);
    }
    
    private void Update()
    {
        for (int i = 0; i < biomeOneHeight; i++)
        {
            for (int j = 0; j < biomeOneSize; j++)
            {
                for (int k = 0; k < biomeOneSize; k++)
                {
                    if (Vector3.Distance(player.position, chunks[i, j, k].transform.position) < renderRange)
                    {
                        chunks[i, j, k].SetActive(true);
                    }
                    else
                    {
                        chunks[i, j, k].SetActive(false);
                    }
                }
            }
        }
    }

}


