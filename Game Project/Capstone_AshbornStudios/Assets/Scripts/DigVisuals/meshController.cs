using UnityEngine;

public class meshController : MonoBehaviour
{
    public float radius = 2f;
    public float deformStr = 2;
    public Mesh mesh;
    Vector3[] vertices, modVerts;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        modVerts = mesh.vertices;
    }
    void recalMesh()
    {
        mesh.vertices = modVerts;
        GetComponent<MeshCollider>().sharedMesh = mesh;
        mesh.RecalculateNormals();
    }
    // Update is called once per frame
    private void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                print("here");
                for (int i = 0; i < modVerts.Length; i++)
                {
                    print(i);
                    Vector3 distance = modVerts[i] - hit.point;
                    float smoothingFactor = 2f;

                    float force = deformStr / (1f + hit.point.sqrMagnitude);

                    if (distance.sqrMagnitude < radius)
                    {
                        print("here");
                        modVerts[i] = modVerts[i] + (Vector3.up * force) / smoothingFactor;
                        recalMesh();

                    }
                }
            }
        }
    }
}
