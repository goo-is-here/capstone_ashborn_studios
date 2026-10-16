using UnityEngine;
using TMPro;
public class tutorialScript : MonoBehaviour
{
    [TextArea]
    [SerializeField] string[] tutorialInformation;
    [SerializeField] TextMeshProUGUI text;
    public int stringToDisplay;
    [SerializeField] GameObject leftArrow;
    [SerializeField] GameObject rightArrow;

    // Update is called once per frame
    void Update()
    {
        stringToDisplay = Mathf.Clamp(stringToDisplay, 0, tutorialInformation.Length-1);
        if(stringToDisplay == 0)
        {
            leftArrow.SetActive(false);
        }
        else
        {
            leftArrow.SetActive(true);
        }
        if(stringToDisplay >= tutorialInformation.Length-1)
        {
            rightArrow.SetActive(false);
        }
        else
        {
            rightArrow.SetActive(true);
        }
        
        text.text = tutorialInformation[stringToDisplay];
    }
}
