using UnityEngine;

public class SpawnMineralPopup : MonoBehaviour
{
    [Header("Refrences")]
    public Transform popupContainer;
    public GameObject popupPrefab;

    public void DisplayCollectible(string itemName, Sprite itemIcon, int numCollected)
    {
        GameObject newPopup = Instantiate(popupPrefab, popupContainer);

        MineralPopup popupScript = newPopup.GetComponent<MineralPopup>();
        popupScript.SpawnPopup(itemName, itemIcon, numCollected);
    }


}
