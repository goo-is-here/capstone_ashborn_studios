using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MarchingCubes : MonoBehaviour
{
    [SerializeField] private int width = 10;
    [SerializeField] private int height = 10;
    [SerializeField] private float heightThreshold = 0.5f;
    [SerializeField] private float noise = 1;
    [SerializeField] private bool visualizeNoise;

    private float[,,] heights;

    private MeshFilter meshFilter;

    private List<Vector3> vertices = new List<Vector3>();
    private List<int> triangles = new List<int>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        StartCoroutine(UpdateAll());
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

                    MarchCube(new Vector3(x, y, z), GetConfig(cubeCorners));
                }
            }
        }
    }

    private void MarchCube(Vector3 pos, int index)
    {
        if(index == 0 || index == 255)
        {
            return;
        }
        int edgeIndex = 0;
        for(int i = 0; i < 5; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                int triTable = MarchingTable.Triangles[index, edgeIndex];
                if(triTable == -1)
                {
                    return;
                }
                Vector3 start = pos + MarchingTable.Edges[triTable, 0];
                Vector3 end = pos + MarchingTable.Edges[triTable, 1];
                Vector3 vertex = (start + end) / 2;
                vertices.Add(vertex);
                triangles.Add(vertices.Count - 1);

                edgeIndex++;
            }
        }
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
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;
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
                    float currentHeight = height * calculateNoise(noise, x, y, z);
                    
                    float newHeight;
                    if(y > currentHeight)
                    {
                        newHeight = y - currentHeight;
                    }
                    else
                    {
                        newHeight = currentHeight - y;
                    }
                    heights[x, y, z] = newHeight;
                }
            }
        }
    }
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
}
