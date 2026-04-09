using UnityEngine;

public class setBlock : MonoBehaviour
{
    public Material dirt, crumStone;
    public float dirtTransStart = -10f, dirtTransMid = -20f, dirtTransEnd = -30f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setTheBlock(float yCoord, MeshRenderer mesh, diggableBlock digScript)
    {
        Material newMat;
        print(yCoord);
        if (yCoord >= dirtTransStart)
        {
            newMat = dirt;
        }
        else if(yCoord >= dirtTransMid)
        {
            float denominator = dirtTransMid - dirtTransStart;
            float chance = (((yCoord - dirtTransStart) / denominator) / 2) * 100;
            float hit = Random.Range(0f, 100f);
            if(chance <= hit)
            {
                newMat = dirt;
            }
            else
            {
                newMat = crumStone;
            }
        }
        else if(yCoord >= dirtTransEnd)
        {
            float denominator = dirtTransEnd - dirtTransMid;
            float chance = (((yCoord - dirtTransMid) / denominator) / 2) * 100;
            float hit = Random.Range(0f, 100f);
            if (chance <= hit)
            {
                newMat = crumStone;
            }
            else
            {
                newMat = dirt;
            }
        }
        else
        {
            newMat = crumStone;
        }
        mesh.material = newMat;
    }
}
