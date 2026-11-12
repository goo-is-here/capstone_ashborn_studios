using UnityEngine;

public class collisionChecker : MonoBehaviour
{
    characterInventory invent;
    [SerializeField] MeshCollider col;
    [SerializeField] MeshRenderer rend;
    [SerializeField] Material placingMaterial;
    [SerializeField] Material collisionMaterial;
    [SerializeField] Material standardMaterial;
    bool canPlace = true;
    public bool placed = false;
    furnitureManager furnitureManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        invent = GameObject.FindGameObjectWithTag("Inventory").GetComponent<characterInventory>();
        furnitureManager = GameObject.FindGameObjectWithTag("FurnitureManager").GetComponent<furnitureManager>();
        rend.material = placingMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        if(canPlace && Input.GetKeyDown(KeyCode.Return) && !placed)
        {
            placed = true;
            rend.material = standardMaterial;
            col.isTrigger = false;
            furnitureManager.receivePosition(transform.position);
            invent.furnitureToDestroy = null;
            invent.removeCurrentFurniture();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!placed)
        {
            rend.material = placingMaterial;
            canPlace = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!placed)
        {
            rend.material = collisionMaterial;
            canPlace = false;
        }
        
    }
}
