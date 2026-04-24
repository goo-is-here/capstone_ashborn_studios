using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FadeToBlack : MonoBehaviour
{
    public float targetValue = 0;
    CanvasRenderer elementToFade;
    float resetVal;

    void Start()
    {
        elementToFade = gameObject.GetComponent<CanvasRenderer>();
        resetVal = elementToFade.GetAlpha();
        StartCoroutine(LerpFunction(targetValue, 3));
    }
    IEnumerator LerpFunction(float endValue, float duration)
    {
        float time = 0;
        float startValue = elementToFade.GetAlpha();

        while (time < duration)
        {
            elementToFade.SetAlpha(Mathf.Lerp(startValue, endValue, time / duration));

            time += Time.deltaTime;
            yield return null;
        }
        elementToFade.SetAlpha(endValue);
        gameObject.SetActive(false);
    }
}