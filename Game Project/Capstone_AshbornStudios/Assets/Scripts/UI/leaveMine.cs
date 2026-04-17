using UnityEngine;
using UnityEngine.SceneManagement;

public class leaveMine : MonoBehaviour
{
    GameObject player;
    public int sceneIndex = 2;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && Vector3.Distance(player.transform.position, transform.position) < 5)
        {
            print("hi");
            Application.Quit();
            //SceneManager.LoadScene(sceneIndex);
        }
    }
}
