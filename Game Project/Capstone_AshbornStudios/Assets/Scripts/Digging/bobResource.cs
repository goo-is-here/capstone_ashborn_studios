using UnityEngine;
using System.Collections;

public class bobResource : MonoBehaviour
{
    Vector3 startPos;
    Vector3 lowPos;
    Vector3 highPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        lowPos = new Vector3(startPos.x, startPos.y - .6f, startPos.z);
        highPos = new Vector3(startPos.x, startPos.y - .85f, startPos.z);
        StartCoroutine(rotate());
        StartCoroutine(lower());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator rotate()
    {
        while (true)
        {
            Vector3 rotationToAdd = new Vector3(0, .5f, 0);
            transform.Rotate(rotationToAdd);
            yield return new WaitForSeconds(.001f);

        }
    }
    IEnumerator lower()
    {
        float time = 0;
        float duration = 2f;
        while(time < duration)
        {
            transform.position = Vector3.Lerp(highPos, lowPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        
        
        StartCoroutine(raise());
    }
    IEnumerator raise()
    {
        float time = 0;
        float duration = 2f;
        while (time < duration)
        {
            transform.position = Vector3.Lerp(lowPos, highPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(lower());
    }

}
