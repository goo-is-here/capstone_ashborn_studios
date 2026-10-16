using UnityEngine;

public class arrowButton : MonoBehaviour
{
    [SerializeField] bool isLeft;
    [SerializeField] tutorialScript tut;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip select;
    public void changeCounter()
    {
        if (isLeft)
        {
            tut.stringToDisplay--;
        }
        else
        {
            tut.stringToDisplay++;
        }
        if (!source.isPlaying)
        {
            source.PlayOneShot(select);
        }
    }
}
