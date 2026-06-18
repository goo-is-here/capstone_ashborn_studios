using UnityEngine;
using System.Collections;
using TMPro;

public class diggableBlock : MonoBehaviour
{
    //block variables
    [Header("Block Variables, do not change here")]
    public float blockHealth = 100f;
    public float minDamage;
    public float currHealth;
    public GameObject particles;
    public GameObject dropped;
    //public AudioSource audioSource;
    AudioClip breaking;
    AudioClip broke;

    //player items
    GameObject player;
    PlayerController controller;
    
    //for setting the variables
    setBlock setter;
    
    void Start()
    {
        //finds player and controller
        player = GameObject.FindGameObjectWithTag("Player");
        controller = player.GetComponent<PlayerController>();
        //current health is set to the max health
        currHealth = blockHealth;
        //gets the block setter
        GameObject set = GameObject.FindGameObjectWithTag("SetBlock");
        //makes sure its not null
        if(set != null)
        {
            setter = set.GetComponent<setBlock>();
        }
    }
    public void hitBlock(float damageVal, Vector3 position)
    {
        //makes sure the minimum damage requirement is met and the durability is not 0
        if(damageVal >= minDamage && controller.getDur() > 0)
        {
            //removes the damage from tool
            currHealth -= damageVal;
            //reduces durability by 1
            if(damageVal <= 1000)
            {
                controller.durabilityChange(-1f);
            }
            //if the damage exceeds the health start breaking
            if (currHealth <= 0)
            {
                //disables the visuals
                this.GetComponent<MeshRenderer>().enabled = false;
                this.GetComponent<Collider>().enabled = false;
                //gets the position to add to mined blocks
                Vector3 pos = this.transform.position;
                if(setter != null)
                {
                    setter.blockPosition.Add(pos);
                }
                //if particles have been set, spawn them
                if(particles != null)
                {
                    GameObject part = Instantiate(particles, pos, Quaternion.identity, transform.parent);
                    Destroy(part, 3f);
                }
                //if dropped has been set, spawn them
                if(dropped != null){
                    GameObject drop = Instantiate(dropped, pos, Quaternion.identity, transform.parent);
                    Destroy(drop, 10f);
                }
                //destroy blocks
                Destroy(gameObject);
            }
        }
        //announces if the tool is broken
        else if(controller.getDur() <= 0)
        {
            string print = "Gah! My tool is in need of repair!";
            controller.printText(print);
        }
        //announces minimum damage requirement
        else
        {
            string print = "Gah! I need an upgrade that does " + minDamage + " to break this block!";
            controller.printText(print);
        }
    }
    //get the digging clip to send to player
    public AudioClip getDigClip()
    {
        return breaking;
    }
    //sets the block variables from setter
    public void setBlock(float health, float min, GameObject drop, GameObject part, AudioClip clip, AudioClip broke2)
    {
        blockHealth = health;
        minDamage = min;
        dropped = drop;
        particles = part;
        breaking = clip;
        broke = broke2;
    }
}
