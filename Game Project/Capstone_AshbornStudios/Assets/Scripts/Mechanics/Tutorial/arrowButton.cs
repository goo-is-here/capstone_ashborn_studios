using UnityEngine;

public class arrowButton : MonoBehaviour
{
    [SerializeField] bool isLeft;
    [SerializeField] tutorialScript tut;
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
    }
}
