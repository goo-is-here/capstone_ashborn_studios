using UnityEngine;
using System.Collections;

public class diggableBlock : MonoBehaviour
{
    public float blockHealth = 100f;
    public float currHealth;
    Vector3 startPos;
    public float moveAmount;
    public float moveSpeed;
    GameObject player;
    MaterialPropertyBlock outlineOn;
    MaterialPropertyBlock outlineOff;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        outlineOn = new MaterialPropertyBlock();
        outlineOn.SetFloat("_outlineScale", 1.01f);
        outlineOff = new MaterialPropertyBlock();
        outlineOff.SetFloat("_outlineScale", 0.0f);
        currHealth = blockHealth;
        startPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        
    }
    private void Update()
    {
        if(Vector3.Distance(player.transform.position, transform.position) > 10)
        {
            this.GetComponent<MeshRenderer>().SetPropertyBlock(outlineOff, 1);
            print(outlineOff.GetFloat("_outlineScale"));
        }
        else
        {
            this.GetComponent<MeshRenderer>().SetPropertyBlock(outlineOn, 1);
            print(outlineOn.GetFloat("_outlineScale"));
        }
    }
    public void hitBlock(float damageVal)
    {
        currHealth -= damageVal;
        //blockMaterials[0].color = new Color(1f - (currHealth / blockHealth), 1f + (currHealth / blockHealth), 0);
        //StartCoroutine(shimmy());
    }
    IEnumerator shimmy()
    {
        transform.position = Vector3.Lerp(startPos, new Vector3(startPos.x + moveAmount, startPos.y, startPos.z), moveSpeed);
        yield return new WaitForSeconds(.1f);
        transform.position = Vector3.Lerp(transform.position, new Vector3(startPos.x - moveAmount*2.5f, startPos.y , startPos.z), moveSpeed);
        yield return new WaitForSeconds(.1f);
        transform.position = Vector3.Lerp(transform.position, startPos, moveSpeed);
        if(currHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
