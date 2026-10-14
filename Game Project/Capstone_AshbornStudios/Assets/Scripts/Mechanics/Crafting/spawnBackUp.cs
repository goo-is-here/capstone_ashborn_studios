using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class spawnBackUp : MonoBehaviour
{
    [Header("Terrain")]
    [SerializeField] private int chunkSize = 50;
    [SerializeField] private float noiseScale = 0.05f;
    [SerializeField, Range(0f, 1f)] private float threshold = 0.5f;
    [SerializeField] private Material material;


    [Header("Surface Interpolation")]
    [SerializeField, Range(0f, 1f)]
    private float interpolationBlend = 1f;

    [Header("Destruction")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float breakRadius = 4f;
    [SerializeField] private float breakRange = 100f;
    [SerializeField] private KeyCode breakKey = KeyCode.Mouse0;

    private GameObject terrainObject;
    private Mesh terrainMesh;
    private MeshCollider terrainCollider;

    private float[,,] density;
    private int gridSize;

    private List<Vector3> vertices;
    private List<int> triangles;
    private Dictionary<EdgeKey, int> edgeCache;

    private static readonly Vector3Int[] Corners =
    {
        new Vector3Int(0, 0, 0),
        new Vector3Int(1, 0, 0),
        new Vector3Int(1, 1, 0),
        new Vector3Int(0, 1, 0),
        new Vector3Int(0, 0, 1),
        new Vector3Int(1, 0, 1),
        new Vector3Int(1, 1, 1),
        new Vector3Int(0, 1, 1)
    };

    private static readonly int[,] Tetrahedra =
    {
        { 0, 5, 1, 6 },
        { 0, 1, 2, 6 },
        { 0, 2, 3, 6 },
        { 0, 3, 7, 6 },
        { 0, 7, 4, 6 },
        { 0, 4, 5, 6 }
    };

    private struct EdgeKey
    {
        public int A;
        public int B;

        public EdgeKey(int a, int b)
        {
            A = Mathf.Min(a, b);
            B = Mathf.Max(a, b);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is EdgeKey))
                return false;

            EdgeKey other = (EdgeKey)obj;
            return A == other.A && B == other.B;
        }

        public override int GetHashCode()
        {
            unchecked { return (A * 397) ^ B; }
        }
    }
    private void Start()
    {
        Generate();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            Generate();

        if (Input.GetKeyDown(breakKey))
            BreakTerrain();
    }

    private void Generate()
    {
        float startTime = Time.realtimeSinceStartup;

        if (chunkSize < 1)
            return;

        ClearTerrain();

        gridSize = chunkSize + 1;
        density = new float[gridSize, gridSize, gridSize];

        // Sample the terrain density.
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                for (int z = 0; z < gridSize; z++)
                {
                    Vector3 worldPosition = transform.TransformPoint(
                        new Vector3(x, y, z)
                    );

                    float value = Perlin3D(
                        worldPosition.x * noiseScale,
                        worldPosition.y * noiseScale,
                        worldPosition.z * noiseScale
                    );

                    density[x, y, z] = value;
                }
            }
        }

        RebuildMesh();

        Debug.Log(
            $"Terrain generated in " +
            $"{Time.realtimeSinceStartup - startTime:F2} seconds."
        );
    }

    private void BreakTerrain()
    {
        if (density == null || terrainObject == null)
            return;

        Camera cam = playerCamera != null
            ? playerCamera
            : Camera.main;

        if (cam == null)
        {
            Debug.LogWarning("Assign a player camera in the Inspector.");
            return;
        }

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out RaycastHit hit, breakRange))
            return;

        if (hit.collider.gameObject != terrainObject)
            return;

        // Convert the hit into terrain-local grid coordinates.
        Vector3 localHit =
            terrainObject.transform.InverseTransformPoint(hit.point);

        // Clamp the radius to a sensible positive value.
        float radius = Mathf.Max(0.1f, breakRadius);

        int minX = Mathf.Max(0, Mathf.FloorToInt(localHit.x - radius));
        int maxX = Mathf.Min(chunkSize, Mathf.CeilToInt(localHit.x + radius));

        int minY = Mathf.Max(0, Mathf.FloorToInt(localHit.y - radius));
        int maxY = Mathf.Min(chunkSize, Mathf.CeilToInt(localHit.y + radius));

        int minZ = Mathf.Max(0, Mathf.FloorToInt(localHit.z - radius));
        int maxZ = Mathf.Min(chunkSize, Mathf.CeilToInt(localHit.z + radius));

        float radiusSquared = radius * radius;
        bool changed = false;

        // Remove density samples inside the destruction sphere.
        for (int x = minX; x <= maxX; x++)
        {
            for (int y = minY; y <= maxY; y++)
            {
                for (int z = minZ; z <= maxZ; z++)
                {
                    Vector3 sample = new Vector3(x, y, z);

                    if (density[x, y, z] < threshold)
                        changed = true;

                    density[x, y, z] = 1f;
                }
            }
        }

        if (changed)
            RebuildMesh();
    }

    private void RebuildMesh()
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();
        edgeCache = new Dictionary<EdgeKey, int>();

        // Polygonize all grid cells from the current density.
        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                for (int z = 0; z < chunkSize; z++)
                {
                    PolygonizeCube(x, y, z);
                }
            }
        }

        // Create the terrain object once and reuse it.
        if (terrainObject == null)
        {
            terrainObject = new GameObject("MarchingTerrain");
            terrainObject.transform.SetParent(transform, false);

            terrainObject.AddComponent<MeshFilter>();

            MeshRenderer renderer =
                terrainObject.AddComponent<MeshRenderer>();

            if (material != null)
                renderer.sharedMaterial = material;

            terrainCollider =
                terrainObject.AddComponent<MeshCollider>();
        }

        MeshFilter filter = terrainObject.GetComponent<MeshFilter>();

        Mesh newMesh = new Mesh();
        newMesh.name = "Breakable Terrain";

        if (vertices.Count > 65535)
            newMesh.indexFormat = IndexFormat.UInt32;

        newMesh.SetVertices(vertices);
        newMesh.SetTriangles(triangles, 0);
        newMesh.RecalculateNormals();
        newMesh.RecalculateBounds();

        // Detach the old mesh before replacing it.
        terrainCollider.sharedMesh = null;
        filter.sharedMesh = newMesh;
        terrainCollider.sharedMesh = newMesh;

        if (terrainMesh != null)
            Destroy(terrainMesh);

        terrainMesh = newMesh;
    }

    private void PolygonizeCube(int x, int y, int z)
    {
        Vector3[] positions = new Vector3[8];
        float[] values = new float[8];
        int[] ids = new int[8];

        for (int i = 0; i < 8; i++)
        {
            Vector3Int c = Corners[i];

            int px = x + c.x;
            int py = y + c.y;
            int pz = z + c.z;

            positions[i] = new Vector3(px, py, pz);
            values[i] = density[px, py, pz];
            ids[i] = GridId(px, py, pz);
        }

        for (int t = 0; t < 6; t++)
        {
            Vector3[] p = new Vector3[4];
            float[] v = new float[4];
            int[] id = new int[4];

            for (int i = 0; i < 4; i++)
            {
                int corner = Tetrahedra[t, i];
                p[i] = positions[corner];
                v[i] = values[corner];
                id[i] = ids[corner];
            }

            PolygonizeTetrahedron(p, v, id);
        }
    }

    private void PolygonizeTetrahedron(
        Vector3[] p, float[] v, int[] ids)
    {
        List<int> inside = new List<int>(4);
        List<int> outside = new List<int>(4);

        for (int i = 0; i < 4; i++)
        {
            if (v[i] < threshold)
                inside.Add(i);
            else
                outside.Add(i);
        }

        if (inside.Count == 0 || inside.Count == 4)
            return;

        if (inside.Count == 1 || outside.Count == 1)
        {
            bool invert = inside.Count == 3;
            int lone = invert ? outside[0] : inside[0];
            List<int> other = invert ? inside : outside;

            int a = GetEdgeVertex(lone, other[0], p, v, ids);
            int b = GetEdgeVertex(lone, other[1], p, v, ids);
            int c = GetEdgeVertex(lone, other[2], p, v, ids);

            AddTriangle(a, b, c, invert);
        }
        else
        {
            int i0 = inside[0];
            int i1 = inside[1];
            int o0 = outside[0];
            int o1 = outside[1];

            int a = GetEdgeVertex(i0, o0, p, v, ids);
            int b = GetEdgeVertex(i0, o1, p, v, ids);
            int c = GetEdgeVertex(i1, o1, p, v, ids);
            int d = GetEdgeVertex(i1, o0, p, v, ids);

            AddTriangle(a, b, c, false);
            AddTriangle(a, c, d, false);
        }
    }

    private int GetEdgeVertex(
        int a, int b, Vector3[] p, float[] v, int[] ids)
    {
        EdgeKey key = new EdgeKey(ids[a], ids[b]);

        if (edgeCache.TryGetValue(key, out int existing))
            return existing;

        float denominator = v[b] - v[a];

        float t = Mathf.Abs(denominator) < 0.000001f
            ? 0.5f
            : (threshold - v[a]) / denominator;

        t = Mathf.Clamp01(t);

        Vector3 intersection = Vector3.Lerp(p[a], p[b], t);

        Vector3 point = Vector3.Lerp(
            p[a], intersection, interpolationBlend);

        int index = vertices.Count;
        vertices.Add(point);
        edgeCache.Add(key, index);

        return index;
    }

    private void AddTriangle(int a, int b, int c, bool invert)
    {
        triangles.Add(a);

        if (invert)
        {
            triangles.Add(c);
            triangles.Add(b);
        }
        else
        {
            triangles.Add(b);
            triangles.Add(c);
        }
    }

    private int GridId(int x, int y, int z)
    {
        return x + gridSize * (y + gridSize * z);
    }


    public static float Perlin3D(float x, float y, float z)
    {
        float ab = Mathf.PerlinNoise(x, y);
        float bc = Mathf.PerlinNoise(y, z);
        float ac = Mathf.PerlinNoise(x, z);

        float ba = Mathf.PerlinNoise(y, x);
        float cb = Mathf.PerlinNoise(z, y);
        float ca = Mathf.PerlinNoise(z, x);

        return (ab + bc + ac + ba + cb + ca) / 6f;
    }

    private void ClearTerrain()
    {
        if (terrainObject != null)
            Destroy(terrainObject);

        terrainObject = null;
        terrainCollider = null;

        if (terrainMesh != null)
            Destroy(terrainMesh);

        terrainMesh = null;
        density = null;
    }

    private void OnDestroy()
    {
        ClearTerrain();
    }
}