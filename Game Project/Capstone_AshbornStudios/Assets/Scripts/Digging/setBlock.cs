using UnityEngine;

public class setBlock : MonoBehaviour
{
    public Material crumStone, rootStone;
    public float dirtTransStart = -10f, dirtTransMid = -20f, dirtTransEnd = -30f;
    [Header("Crumstone Variables")]
    [SerializeField]
    float crumHealth = 100f;
    [SerializeField]
    float crumMinDamage = 10f;
    [SerializeField]
    GameObject crumDrop;
    [SerializeField]
    GameObject crumParticles;
    [Header("Rootstone Variables")]
    [SerializeField]
    float rootHealth = 150f;
    [SerializeField]
    float rootMinDamage = 15f;
    [SerializeField]
    GameObject rootDrop;
    [SerializeField]
    GameObject rootParticles;
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
        if (yCoord >= dirtTransStart)
        {
            newMat = crumStone;
            setCrumStone(digScript);
        }
        else if(yCoord >= dirtTransMid)
        {
            float denominator = dirtTransMid - dirtTransStart;
            float chance = (((yCoord - dirtTransStart) / denominator) / 2) * 100;
            float hit = Random.Range(0f, 100f);
            if(chance <= hit)
            {
                newMat = crumStone;
                setCrumStone(digScript);
            }
            else
            {
                newMat = rootStone;
                setRootStone(digScript);
            }
        }
        else if(yCoord >= dirtTransEnd)
        {
            float denominator = dirtTransEnd - dirtTransMid;
            float chance = (((yCoord - dirtTransMid) / denominator) / 2) * 100;
            float hit = Random.Range(0f, 100f);
            if (chance <= hit)
            {
                newMat = rootStone;
                setRootStone(digScript);
            }
            else
            {
                newMat = crumStone;
                setCrumStone(digScript);
            }
        }
        else
        {
            newMat = rootStone;
            setRootStone(digScript);
        }
        mesh.material = newMat;
    }
    void setCrumStone(diggableBlock digScript)
    {
        digScript.setBlock(diggableBlock.blockType.CRUMBSTONE, crumHealth, crumMinDamage, crumDrop, crumParticles);
    }
    void setRootStone(diggableBlock digScript)
    {
        digScript.setBlock(diggableBlock.blockType.ROOTSTONE, rootHealth, rootMinDamage, rootDrop, rootParticles);
    }
}
