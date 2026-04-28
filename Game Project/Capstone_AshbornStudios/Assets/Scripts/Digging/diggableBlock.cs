using UnityEngine;
using System.Collections;
using TMPro;

public class diggableBlock : MonoBehaviour
{
    GameObject player;
    public float blockHealth = 100f;
    public float minDamage;
    public float currHealth;
    
    PlayerController controller;
    public GameObject particles;
    public GameObject dropped;
    //public AudioSource audioSource;
    AudioClip breaaking;
    AudioClip broke;
    setBlock setter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        controller = player.GetComponent<PlayerController>();
        currHealth = blockHealth;
        setter = GameObject.FindGameObjectWithTag("SetBlock").GetComponent<setBlock>();
    }
    public void hitBlock(float damageVal, Vector3 position)
    {
        print("here1");
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
                this.GetComponent<Collider>().enabled = false;
                Vector3 pos = this.transform.position;
                setter.blockPosition.Add(pos);
                if(particles != null)
                {
                    GameObject part = Instantiate(particles, pos, Quaternion.identity, transform.parent);
                    Destroy(part, 3f);
                }
                if(dropped != null){
                    GameObject drop = Instantiate(dropped, pos, Quaternion.identity, transform.parent);
                    Destroy(drop, 10f);
                }
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
    public void setBlock(float health, float min, GameObject drop, GameObject part)
    {
        blockHealth = health;
        minDamage = min;
        dropped = drop;
        particles = part;
    }
}
