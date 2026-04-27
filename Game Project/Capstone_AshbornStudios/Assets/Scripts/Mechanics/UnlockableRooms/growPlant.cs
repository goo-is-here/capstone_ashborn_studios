using UnityEngine;
using System.Collections;

public class growPlant : MonoBehaviour
{
    public float targetScale;
    public float timeToLerp = 0.25f;
    float scaleModifier = 1;
    Transform player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        Vector3 scaleTarget = new Vector3(1, 1, 1)
        if(Vector3.Distance(player.position, transform.position) < .2f && Vector3.Distance(transform.localScale, scaleTarget) <= .1f)
        {
            resetScale();
        }
    }
    public void resetScale()
    {
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        StartCoroutine(LerpFunction(targetScale, timeToLerp));
    }

    IEnumerator LerpFunction(float endValue, float duration)
    {
        float time = 0;
        float startValue = scaleModifier;
        Vector3 startScale = transform.localScale;

        while (time < duration)
        {
            scaleModifier = Mathf.Lerp(startValue, endValue, time / duration);
            transform.localScale = startScale * scaleModifier;
            time += Time.deltaTime;
            yield return null;
        }

        transform.localScale = startScale * endValue;
        scaleModifier = endValue;
    }
}
