using UnityEngine;
using System.Collections;
using TMPro;

public class diggableBlock : MonoBehaviour
{
    GameObject player;
    float blockHealth = 100f;
    float minDamage;
    public float currHealth;
    
    PlayerController controller;
    public GameObject particles;
    public GameObject dropped;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        controller = player.GetComponent<PlayerController>();
        currHealth = blockHealth;
    }
    public void hitBlock(float damageVal, Vector3 position)
    {
        if(damageVal >= minDamage && controller.getDur() > 0)
        {
            currHealth -= damageVal;
            // moveVertices(position);
            //blockMaterials[0].color = new Color(1f - (currHealth / blockHealth), 1f + (currHealth / blockHealth), 0);
            //StartCoroutine(shimmy());
            controller.durabilityChange(-1f);
            if (currHealth <= 0)
            {
                this.GetComponent<MeshRenderer>().enabled = false;
                this.GetComponent<BoxCollider>().enabled = false;
                Vector3 pos = this.transform.position;
                GameObject part = Instantiate(particles, pos, Quaternion.identity, transform.parent);
                GameObject drop = Instantiate(dropped, pos, Quaternion.identity, transform.parent);
                Destroy(part, 3f);
                Destroy(drop, 10f);
                Destroy(gameObject);
            }
        }
        else if(controller.getDur() <= 0)
        {
            string print = "Gah! My tool is in need of repair!";
            controller.printText(print);
        }
        else
        {
            string print = "Gah! I need an upgrade that does " + minDamage + " to break this block!";
            controller.printText(print);
        }
    }
    public void setBlock(float health, float min)
    {
        blockHealth = health;
        minDamage = min;
    }
}
