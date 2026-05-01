using UnityEngine;

public class stopFall : MonoBehaviour
{
    [SerializeField] GameObject firstBlock;
    public float adjustx;
    public float adjustz;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        
    }
    private void Update()
    {
        if(firstBlock != null)
        {
            firstBlock.transform.position = new Vector3(((int)(transform.localPosition.x + .5f) / 3) * 3, ((int)(transform.localPosition.y - 12f) / 3) * 3, ((int)(transform.localPosition.z + 4) / 3) * 3);

            firstBlock.GetComponent<SpawnBlocks>().enabled = true;
            for (int i = 0; i < firstBlock.GetComponent<SpawnBlocks>().spawnedBlocks.Length; i++)
            {
                firstBlock.GetComponent<SpawnBlocks>().spawnedBlocks[i] = false;
            }
            firstBlock.GetComponent<SpawnBlocks>().spawnNeighbors();
            this.enabled = false;
            print(firstBlock.transform.position);

        }
        
    }


}
