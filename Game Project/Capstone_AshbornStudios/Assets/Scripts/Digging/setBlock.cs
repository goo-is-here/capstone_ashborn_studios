using UnityEngine;

public class setBlock : MonoBehaviour
{
    [SerializeField]
    GameObject blockPrefab;
    [SerializeField, Range(0, 1)]
    float airThreshold;
    [SerializeField]
    float noiseScale;
    public float dirtTransStart = -10f, dirtTransEnd = -30f;

    [Header("Cavern Blocks Information")]
    [SerializeField]
    BlockObject[] cavernBlocks;
    [SerializeField]
    float[] cavernSpawnRates;

    [Header("Lush Basin Blocks Information")]
    [SerializeField]
    BlockObject[] lushBlocks;
    [SerializeField]
    float[] lushSpawnRates;

    public void setTheBlock(Vector3 position, MeshRenderer mesh, SpawnBlocks spawnBlock, diggableBlock block)
    {
        int biomeSelect = Random.Range(0, 1);
        float noise = calculateNoise(position.x, position.y, position.z);
        if(noise <= airThreshold)
        {
            spawnBlock.setAir();
        }
        if(position.y > dirtTransStart)
        {
            cavernBiomeSet(noise, mesh, spawnBlock, block);
        }
        else if(position.y > dirtTransEnd)
        {
            if(biomeSelect == 0)
            {
                cavernBiomeSet(noise, mesh, spawnBlock, block);
            }
            else
            {
                lushBiomeSet(noise, mesh, spawnBlock, block);
            }
        }
        else
        {
            lushBiomeSet(noise, mesh, spawnBlock, block);
        }
    }
    float calculateNoise(float x, float y, float z)
    {
        float xy = Mathf.PerlinNoise(x, y);
        float yz = Mathf.PerlinNoise(y, z);
        float zx = Mathf.PerlinNoise(z, x);
        float yx = Mathf.PerlinNoise(y, x);
        float zy = Mathf.PerlinNoise(z, y);
        float xz = Mathf.PerlinNoise(x, z);
        float average = (xy + yz + zx + yx + zy + xz) / 6f;
        return average;
    }
    void cavernBiomeSet(float noise, MeshRenderer mesh, SpawnBlocks spawnBlock, diggableBlock block)
    {
        int index = 0;
        for(int i = 0; i < cavernSpawnRates.Length; i++)
        {
            if(Mathf.Abs(cavernSpawnRates[i] - noise) < Mathf.Abs(cavernSpawnRates[index] - noise))
            {
                index = i;
            }
        }
        mesh.material = cavernBlocks[index].mat;
        spawnBlock.setBlock(cavernBlocks[index].type);
        block.setBlock(cavernBlocks[index].blockHealth, cavernBlocks[index].minDamage, cavernBlocks[index].dropped, cavernBlocks[index].particles);
    }
    void lushBiomeSet(float noise, MeshRenderer mesh, SpawnBlocks spawnBlock, diggableBlock block)
    {
        int index = 0;
        for (int i = 0; i < lushSpawnRates.Length; i++)
        {
            if (Mathf.Abs(lushSpawnRates[i] - noise) < Mathf.Abs(lushSpawnRates[index] - noise))
            {
                index = i;
            }
        }
        mesh.material = lushBlocks[index].mat;
        spawnBlock.setBlock(lushBlocks[index].type);
        block.setBlock(lushBlocks[index].blockHealth, lushBlocks[index].minDamage, lushBlocks[index].dropped, lushBlocks[index].particles);
    }
}
