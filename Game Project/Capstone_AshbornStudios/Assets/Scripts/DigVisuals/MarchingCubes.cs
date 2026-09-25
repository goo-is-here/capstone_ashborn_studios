using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MarchingCubes : MonoBehaviour
{
    [SerializeField] public int width = 10;
    [SerializeField] public int height = 10;
    [SerializeField] private float heightThreshold = 0.5f;
    [SerializeField] private float noise = 1;
    [SerializeField] private bool visualizeNoise;
    [SerializeField] private int biomeOneSize = 10;
    [SerializeField] private int biomeOneHeight = 4;
    [SerializeField] private GameObject chunkPrefab;
    [SerializeField] private Transform player;
    [SerializeField] private int renderRange;
    private GameObject[,,] chunks;
    bool[,,] deleted;
    private float[,,] heights;

    private MeshFilter meshFilter;

    private List<Vector3> vertices = new List<Vector3>();
    private List<int> triangles = new List<int>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        deleted = new bool[width + 1, height + 1, width + 1];
        chunks = new GameObject[biomeOneHeight, biomeOneSize, biomeOneSize];
        meshFilter = GetComponent<MeshFilter>();
        setHeights();
        MarchCubes();
        SetMesh();
        //StartCoroutine(UpdateAll());
    }
    private IEnumerator UpdateAll()
    {
        while (true)
        {
            setHeights();
            MarchCubes();
            SetMesh();
            yield return new WaitForSeconds(1);
        }
    }
    private void MarchCubes()
    {
        vertices.Clear();
        triangles.Clear();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int z = 0; z < width; z++)
                {
                    float[] cubeCorners = new float[8];

                    for(int i = 0; i < 8; i++)
                    {
                        Vector3Int corner = new Vector3Int(x, y, z) + MarchingTable.Corners[i];
                        cubeCorners[i] = heights[corner.x, corner.y, corner.z];

                    }

                    MarchCube(new Vector3(x, y, z), cubeCorners);
                }
            }
        }
    }

    private void MarchCube(Vector3 pos, float[] cubeCorners)
    {
        int index = GetConfig(cubeCorners);
        if(index == 0 || index == 255)
        {
            return;
        }
        int edgeIndex = 0;
        for(int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                int triTable = MarchingTable.Triangles[index, edgeIndex];
                if (triTable == -1)
                {
                    return;
                }
                Vector3 start = pos + MarchingTable.Edges[triTable, 0];
                Vector3 end = pos + MarchingTable.Edges[triTable, 1];
                Vector3 vertex = Vector3.Lerp(start, end, (heightThreshold - cubeCorners[GetEnd(MarchingTable.Edges[triTable, 0])]) / (cubeCorners[GetEnd(MarchingTable.Edges[triTable, 1])] - cubeCorners[GetEnd(MarchingTable.Edges[triTable, 0])]));
                vertices.Add(vertex);
                triangles.Add(vertices.Count - 1);

                edgeIndex++;
            }
        }
    }
    private int GetEnd(Vector3 pos)
    {
        for(int i = 0; i<MarchingTable.Corners.Length; i++)
        {
            if(pos == MarchingTable.Corners[i])
            {
                return i;
            }
        }
        return default;
    }
    private int GetConfig(float[] cubeCorners)
    {
        int index = 0;

        for(int i = 0; i < 8; i++)
        {
            if(cubeCorners[i] > heightThreshold)
            {
                index |= 1 << i;
            }
        }

        return index;
    }

    private void SetMesh()
    {
        Mesh mesh = new Mesh();
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        meshFilter.sharedMesh = mesh;
        MeshCollider collider = GetComponent<MeshCollider>();
        collider.sharedMaterial = null;
        collider.sharedMesh = mesh;
    }
    private void setHeights()
    {
        heights = new float[width + 1, height + 1, width + 1];
        for (int x = 0; x <= width; x++)
        {
            for(int y = 0; y <= height; y++)
            {
                for(int z = 0; z <= width; z++)
                {
                    float currentHeight = calculateNoise(noise, x, y, z);
                    
                    
                    heights[x, y, z] = currentHeight;
                }
            }
        }
    }
    float calculateNoise(float nosie, int x, int y, int z)
    {
        if (deleted[x, y, z] == true)
        {
            return 0;
        }
        
        float newx = ((float)x + transform.position.x)* (float)nosie;
        float newy = ((float)y + transform.position.y) * (float)nosie;
        float newz = ((float)z + transform.position.z) * (float)nosie;
        float xy = Mathf.PerlinNoise(newx, newy);
        float yz = Mathf.PerlinNoise(newy, newz);
        float zx = Mathf.PerlinNoise(newz, newx);
        float yx = Mathf.PerlinNoise(newy, newx);
        float zy = Mathf.PerlinNoise(newz, newy);
        float xz = Mathf.PerlinNoise(newx, newz);
        float average = (xy + yz + zx + yx + zy + xz) / 6f;
        return average;
    }
    private void OnDrawGizmosSelected()
    {
        if(!visualizeNoise || !Application.isPlaying)
        {
            return;
        }
        for (int x = 0; x <= width; x++)
        {
            for (int y = 0; y <= height; y++)
            {
                for (int z = 0; z <= width; z++)
                {
                    Gizmos.color = new Color(heights[x, y, z], heights[x, y, z], heights[x, y, z]);
                    Gizmos.DrawSphere(new Vector3(x, y, z), 0.2f);
                }
            }
        }
    }
    public void digging(Vector3 position, float range)
    {
        position -= meshFilter.transform.position;

        int i = 0;
        for (int x = 0; x <= width; x++)
        {
            for (int y = 0; y <= height; y++)
            {
                for (int z = 0; z <= width; z++)
                {
                    if(Vector3.Distance(position, vertices[i]) < range)
                    {
                        deleted[x, y, z] = true;
                    }
                    i++;
                }
            }
        }
        setHeights();
        MarchCubes();
        SetMesh();
    }

}
