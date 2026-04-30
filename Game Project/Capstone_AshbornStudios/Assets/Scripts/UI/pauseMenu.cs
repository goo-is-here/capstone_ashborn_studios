using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class pauseMenu : MonoBehaviour
{
    PlayerController cont;
    GameObject pause;
    Vector3 stuckPos;
    [SerializeField]
    GameObject stuck;
    public float targetValue = 0;
    public CanvasRenderer elementToFade;
    public float resetVal;
    public GameObject crafting;
    [SerializeField] AudioClip quitGame;
    AudioSource source;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        resetVal = elementToFade.GetAlpha();
        cont = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        stuckPos = cont.transform.position;
        pause = GameObject.FindGameObjectWithTag("Pause");
        pause.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && crafting != null && !crafting.activeSelf)
        {
            cont.canMove = !cont.canMove;
            pause.SetActive(!pause.activeSelf);
            if(cont.canMove)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            if(SceneManager.GetActiveScene().buildIndex == 2)
            {
                stuck.SetActive(pause.activeSelf);
            }
            else
            {
                stuck.SetActive(false);
            }
        }
    }
    public void stuckHelp()
    {
        StartCoroutine(enter(1, 3));
        elementToFade.gameObject.SetActive(true);
    }
    IEnumerator enter(float endValue, float duration)
    {
        float time = 0;
        float startValue = 0;

        while (time < duration)
        {
            elementToFade.SetAlpha(Mathf.Lerp(startValue, endValue, time / duration));

            time += Time.deltaTime;
            yield return null;
        }
        elementToFade.SetAlpha(endValue);
        StartCoroutine(LerpFunction(0, 3));
        cont.transform.position = stuckPos;
    }
    IEnumerator LerpFunction(float endValue, float duration)
    {
        float time = 0;
        float startValue = 1;
        while (time < duration)
        {
            elementToFade.SetAlpha(Mathf.Lerp(startValue, endValue, time / duration));

            time += Time.deltaTime;
            yield return null;
        }
        elementToFade.SetAlpha(endValue);
        elementToFade.gameObject.SetActive(false);
    }
}
