using UnityEngine;

public class craftingAdd : MonoBehaviour
{
    public int recipeToMake;
    craftingBench bench;
    private void Start()
    {
        bench = GameObject.FindGameObjectWithTag("Crafting").GetComponent<craftingBench>();
    }
    public void add()
    {
        bench.Make(recipeToMake);
    }
}
