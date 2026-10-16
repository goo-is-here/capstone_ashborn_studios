using UnityEngine;

public class closeTut : MonoBehaviour
{
    [SerializeField] GameObject tut;
    public void closeMenu()
    {
        tut.SetActive(false);
    }
}
