using UnityEngine;

public class closeTut : MonoBehaviour
{
    [SerializeField] GameObject tut;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip select;
    public void closeMenu()
    {
        tut.SetActive(false);
        if (!source.isPlaying)
        {
            source.PlayOneShot(select);
        }
    }
}
