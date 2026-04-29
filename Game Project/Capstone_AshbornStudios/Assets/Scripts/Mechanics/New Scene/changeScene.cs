using UnityEngine;
using UnityEngine.SceneManagement;
public class changeScene : MonoBehaviour
{
    public float dist = 2f;
    public int index = 1;
    GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(player.transform.position, transform.position) < dist && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadSceneAsync(index);
        }
    }
}
