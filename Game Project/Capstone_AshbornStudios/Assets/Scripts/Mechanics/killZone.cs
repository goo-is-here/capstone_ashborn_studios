using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class killZone : MonoBehaviour
{
    public float targetValue = 0;
    public CanvasRenderer elementToFade;
    PlayerController cont;
    private void Start()
    {
        cont = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        print("hyere");
        if (other.CompareTag("Player"))
        {
            elementToFade.gameObject.SetActive(true);
            StartCoroutine(enter(1, 3));
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
        cont.transform.localPosition = new Vector3(0, 0, 0);
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
