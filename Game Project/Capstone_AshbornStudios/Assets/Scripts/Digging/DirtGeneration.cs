using UnityEngine;

public class DirtGeneration : MonoBehaviour
{
    public int size;
    [SerializeField] GameObject dirtPrefab;
    GameObject[,,] dirtParticles;
    [HideInInspector] public float dirtSize;
    ChunkGeneration chunk;
    public int chunkBreak = -1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        dirtSize = dirtPrefab.GetComponent<MeshRenderer>().bounds.size.x;
        dirtParticles = new GameObject[size, size, size];
    }
    public void generateDirt(float noise, float heightThreshold)
    {
        for(int i = 0; i < size; i++)
        {
            for(int j = 0; j < size; j++)
            {
                for(int k = 0; k < size; k++)
                {
                    Vector3 dirtPos = new Vector3(transform.position.x + (i * dirtSize), transform.position.y + (j * dirtSize), transform.position.z + (k * dirtSize));
                    if(perlinNoise(noise, dirtPos.x, dirtPos.y, dirtPos.z) > heightThreshold)
                    {
                        GameObject newDirt = Instantiate(dirtPrefab, dirtPos, transform.rotation, transform);
                        dirtParticles[i, j, k] = newDirt;
                    }
                }
            }
        }
        if(chunkBreak != -1)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    for (int k = 0; k < size; k++)
                    {
                        if((chunkBreak & 1) != 1)
                        {
                            Destroy(dirtParticles[i, j, k]);
                        }
                    }
                }
            }
        }    }
    public void sendChunk()
    {
        chunkBreak = 0;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                for (int k = 0; k < size; k++)
                {
                    if(dirtParticles[i,j,k] != null)
                    {
                        chunkBreak = chunkBreak << 1;
                        chunkBreak |= 1;
                    }
                    else
                    {
                        chunkBreak = chunkBreak << 1;
                    }
                }
            }
        }
        if(chunkBreak == 0)
        {
            chunkBreak = -1;
        }
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
