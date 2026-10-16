using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class pauseMenu : MonoBehaviour, IDataPersistence
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
    public GameObject journal;
    public GameObject settingsMenu;
    private bool inSettings;
    bool seenTutorial;
    [SerializeField] GameObject tutorialObject;
    [SerializeField] GameObject stuckbag;
    [SerializeField] AudioClip select;
    [SerializeField] AudioSource source;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        resetVal = elementToFade.GetAlpha();
        cont = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        stuckPos = cont.transform.position;
        pause = GameObject.FindGameObjectWithTag("Pause");
        pause.SetActive(false);
        crafting.SetActive(false);
        if (seenTutorial)
        {
            tutorialObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && crafting != null && !crafting.activeSelf && !journal.GetComponent<Canvas>().enabled)
        {
            print("press");
            cont.canMove = !cont.canMove;
            pause.SetActive(!pause.activeSelf);
            if (settingsMenu.activeSelf) settingsMenu.SetActive(false);
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
        if (!seenTutorial)
        {
            if (tutorialObject.activeSelf)
            {
                cont.canMove = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                cont.canMove = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                tutorialObject.SetActive(false);
                seenTutorial = true;
            }
        }
    }
    public void stuckHelp()
    {
        StartCoroutine(enter(1, 3));
        elementToFade.gameObject.SetActive(true);
        if (!source.isPlaying)
        {
            source.PlayOneShot(select);
        }
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
        Vector3 holder = cont.transform.position;
        cont.transform.position = stuckPos;
        stuckbag.SetActive(true);
        stuckbag.transform.position = holder;
        stuckbag.GetComponent<pickUpBag>().addItems();
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

    public void OpenSettignsMenu()
    {
        settingsMenu.SetActive(true);
        if (!source.isPlaying)
        {
            source.PlayOneShot(select);
        }
    }

    public void CloseSettingsMenu()
    {
        settingsMenu.SetActive(false);
        if (!source.isPlaying)
        {
            source.PlayOneShot(select);
        }
    }
    public void LoadData(GameData data)
    {
        seenTutorial = data.tutorial;
    }
    //save variables into game data
    //Currently saving current scene, position per scene, damage of tool, durability of tool, max durability
    public void SaveData(ref GameData data)
    {
        if (data != null)
        {
            data.tutorial = seenTutorial;
        }

    }
    public void tutorialMenu()
    {
        tutorialObject.SetActive(true);
        if (!source.isPlaying)
        {
            source.PlayOneShot(select);
        }
    }
}
