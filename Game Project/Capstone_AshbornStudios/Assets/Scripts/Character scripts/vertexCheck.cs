using UnityEngine;

public class vertexCheck : MonoBehaviour
{
    Vector3[] vertices;
    Mesh mesh;
    GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        mesh = meshFilter.mesh;
        vertices = mesh.vertices;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            int count = 0;
            for(int i = 0; i < vertices.Length; i++)
            {
                if (Vector3.Distance(player.transform.position, transform.TransformPoint(vertices[i])) < 1f)
                {
                    count++;
                    vertices[i] += Vector3.up * Time.deltaTime;
                }

            }
            mesh.vertices = vertices;
            mesh.RecalculateBounds();
            print(count);
        }
    }
}
