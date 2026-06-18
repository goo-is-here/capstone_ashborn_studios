using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class setBlock : MonoBehaviour, IDataPersistence
{
    //the base block prefab
    [SerializeField]
    GameObject blockPrefab;
    //the noise threshold for air
    [SerializeField, Range(0, 1)]
    float airThreshold;
    //noise threshold for rarity
    [SerializeField, Range(0, 1)]
    float rarityThreshold;
    //scales the noise to add some extra randomness
    [SerializeField]
    float oreNoiseScale;
    [SerializeField]
    float airNoiseScale;
    [SerializeField]
    float rarityNoiseScale;
    //sets biome changes
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

    //list of all mined blocks
    [HideInInspector]
    public List<Vector3> blockPosition;
    Vector3 pos;
    public void setTheBlock(Vector3 position, MeshRenderer mesh, SpawnBlocks spawnBlock, diggableBlock block)
    {
        //gets the position of the block
        pos = position;
        //choses a biome for the transition zone
        int biomeSelect = Random.Range(0, 2);
        //gets the noise with the given position
        float noise = calculateNoise(airNoiseScale, position.x, position.y, position.z);
        //if the noise is in the air threshold, breaks it
        if(noise <= airThreshold)
        {
            spawnBlock.setAir();
            return;
        }
        //recalculates it with the rarity scale
        noise = calculateNoise(rarityNoiseScale, position.x, position.y, position.z);
        //sets it based on the biome with the correct y level
        if (position.y > dirtTransStart)
        {
            cavernBiomeSet(noise, mesh, spawnBlock, block);
        }
        else if (position.y > dirtTransEnd)
        {
            if (biomeSelect == 0)
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
    //loads the mined blocks
    public void LoadData(GameData data)
    {
        foreach(Vector3 pos in data.minedBlocks)
        {
            blockPosition.Add(pos);
        }
    }
    //saves the mined blocks
    public void SaveData(ref GameData data)
    {
        data.minedBlocks.Clear();
        foreach (Vector3 pos in blockPosition)
        {
            data.minedBlocks.Add(pos);
        }
    }
    //calculates a 3d perlin noise using one of each combination
    float calculateNoise(float nosie, float x, float y, float z)
    {
        x = (float)x * (float)nosie;
        y = (float)y * (float)nosie;
        z = (float)z * (float)nosie;
        float xy = Mathf.PerlinNoise(x, y);
        float yz = Mathf.PerlinNoise(y, z);
        float zx = Mathf.PerlinNoise(z, x);
        float yx = Mathf.PerlinNoise(y, x);
        float zy = Mathf.PerlinNoise(z, y);
        float xz = Mathf.PerlinNoise(x, z);
        float average = (xy + yz + zx + yx + zy + xz) / 6f;
        return average;
    }
    //sets the cavern blocks
    void cavernBiomeSet(float noise, MeshRenderer mesh, SpawnBlocks spawnBlock, diggableBlock block)
    {
        int index = 0;
        //if the rarity is high enough grabs a block
        if (noise > rarityThreshold)
        {
            //find what block based on a new noise
            noise = calculateNoise(oreNoiseScale, pos.x, pos.y, pos.z);
            for (int i = 0; i < cavernSpawnRates.Length; i++)
            {
                if (Mathf.Abs(cavernSpawnRates[i] - noise) < Mathf.Abs(cavernSpawnRates[index] - noise))
                {
                    index = i;
                }
            }
        }
        //sets based on given index
        mesh.material = cavernBlocks[index].mat;
        spawnBlock.setBlock(cavernBlocks[index].type);
        block.setBlock(cavernBlocks[index].blockHealth, cavernBlocks[index].minDamage, cavernBlocks[index].dropped, cavernBlocks[index].particles, cavernBlocks[index].breaking, cavernBlocks[index].broke);
    }
    //sets the cavern blocks
    void lushBiomeSet(float noise, MeshRenderer mesh, SpawnBlocks spawnBlock, diggableBlock block)
    {
        int index = 0;
        //if the rarity is high enough grabs a block
        if (noise > rarityThreshold)
        {
            //find what block based on a new noise
            noise = calculateNoise(oreNoiseScale, pos.x, pos.y, pos.z);
            for (int i = 0; i < lushSpawnRates.Length; i++)
            {
                if (Mathf.Abs(lushSpawnRates[i] - noise) < Mathf.Abs(lushSpawnRates[index] - noise))
                {
                    index = i;
                }
            }
        }
        //sets based on given index
        mesh.material = lushBlocks[index].mat;
        spawnBlock.setBlock(lushBlocks[index].type);
        block.setBlock(lushBlocks[index].blockHealth, lushBlocks[index].minDamage, lushBlocks[index].dropped, lushBlocks[index].particles, lushBlocks[index].breaking, lushBlocks[index].broke);
    }
}
