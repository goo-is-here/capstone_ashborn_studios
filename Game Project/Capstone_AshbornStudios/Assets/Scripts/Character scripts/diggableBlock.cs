using UnityEngine;
using System.Collections;

public class diggableBlock : MonoBehaviour
{
    public float blockHealth = 100f;
    public float currHealth;
    public Material test;
    Material texture;
    Vector3 startPos;
    public float moveAmount;
    public float moveSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        texture = new Material(test);
        this.GetComponent<MeshRenderer>().material = texture;
        texture.color = Color.green;
        currHealth = blockHealth;
        startPos = transform.position;
    }
    public void hitBlock(float damageVal)
    {
        currHealth -= damageVal;
        texture.color = new Color(1f - (currHealth / blockHealth), 1f + (currHealth / blockHealth), 0);
        StartCoroutine(shimmy());
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
