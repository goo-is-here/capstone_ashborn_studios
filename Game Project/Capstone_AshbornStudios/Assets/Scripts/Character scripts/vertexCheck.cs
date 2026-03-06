using UnityEngine;

public class vertexCheck : MonoBehaviour
{
    Vector3[] vertices;
    Mesh mesh;
    GameObject player;
    public float digRange = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        mesh = meshFilter.mesh;
        vertices = mesh.vertices;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void moveVertices(Vector3 position)
    {
        print("oops");
        for (int i = 0; i < vertices.Length; i++)
        {
            if (Vector3.Distance(position, transform.TransformPoint(vertices[i])) < digRange)
            {

                vertices[i] += Vector3.up * Time.deltaTime;
                /*
                var direction = transform.TransformPoint(vertices[i]) - position;
                //print("up" + (vertices[i] += Vector3.up));
                print(direction.normalized * 0.01f);
                vertices[i] += direction.normalized * 0.01f;
                */
            }

        }
        mesh.vertices = vertices;
        mesh.RecalculateBounds();
    }
}
