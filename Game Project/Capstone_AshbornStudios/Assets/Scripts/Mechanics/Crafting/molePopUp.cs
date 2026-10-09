using UnityEngine;
using System.Collections;

public class molePopUp : MonoBehaviour
{
    Vector3 lowerPosition;
    [SerializeField] float raiseHeight;
    GameObject player;
    [SerializeField] float distanceToMole;
    [SerializeField] float moleSpeed;
    Vector3 positionToMoveTo;
    bool notUp = true;
    [SerializeField] GameObject store;
    PlayerController cont;
    public MeshRenderer[] playerObjects;
    public SkinnedMeshRenderer[] hands;
    public GameObject playerCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lowerPosition = transform.position;
        positionToMoveTo = new Vector3(transform.position.x, transform.position.y + raiseHeight, transform.position.z);
        player = GameObject.FindGameObjectWithTag("Player");
        cont = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Vector3.Distance(transform.position, player.transform.position) < distanceToMole)
        {
            
            if (cont.canMove)
            {
                playerCamera.gameObject.SetActive(false);
                store.GetComponent<storeScript>().storeCamera.gameObject.SetActive(true);
                foreach(MeshRenderer mesh in playerObjects)
                {
                    mesh.enabled = false;
                }
                foreach(SkinnedMeshRenderer mesh in hands)
                {
                    mesh.enabled = false;
                }
                cont.canMove = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                store.SetActive(true);
                
            }
            else
            {
                playerCamera.gameObject.SetActive(true);
                store.GetComponent<storeScript>().storeCamera.gameObject.SetActive(false);
                foreach (MeshRenderer mesh in playerObjects)
                {
                    mesh.enabled = true;
                }
                foreach (SkinnedMeshRenderer mesh in hands)
                {
                    mesh.enabled = true;
                }
                cont.canMove = true;
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = false;
                store.SetActive(false);
            }
        }
        if (Vector3.Distance(transform.position, player.transform.position) < distanceToMole && notUp)
        {
            notUp = false;
            StartCoroutine(LerpPosition(positionToMoveTo, lowerPosition, moleSpeed));
        }
        else if(Vector3.Distance(transform.position, player.transform.position) > distanceToMole && !notUp)
        {
            notUp = true;
            StartCoroutine(LerpPosition(lowerPosition, positionToMoveTo, moleSpeed));
        }
    }
    
    IEnumerator LerpPosition(Vector3 targetPosition, Vector3 startPosition, float duration)
    {
        float time = 0;

        while (time < duration)
        {
            float t = time / duration;
            t = t * t * (3f - 2f * t);

            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }
}
