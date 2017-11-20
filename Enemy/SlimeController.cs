using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Hero Project G-Slime Class
By: Ryan Su

This class holds the unique behaviors of the slime enemy, including animations and AI

*/

public class SlimeController : Monster
{

    public Animator slimeAnim;

    public Vector2 defaultDir = new Vector2(1, 0);
    public Vector2 newDir;

    public bool noXP = false;


	// Use this for initialization
	void Start ()
    {
        monsterHealth = 65;
        contactDamage = 10;

        player = GameObject.FindGameObjectWithTag("Player");
        slimeAnim = GetComponent<Animator>();
        monsterSprite = GetComponent<SpriteRenderer>();
        monsterRB = GetComponent<Rigidbody2D>();

        //G-Slime Loot Table

        //G-Slime Crystal
        monsterDrops.Add(GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[30]);
        monsterDropChance.Add(25);
        //Arcane Copper Ring
        monsterDrops.Add(GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[13]);
        monsterDropChance.Add(1);
        //Enduring Copper Ring
        monsterDrops.Add(GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[14]);
        monsterDropChance.Add(1);
        //Pendent of Endurance
        monsterDrops.Add(GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[33]);
        monsterDropChance.Add(1);
        //Pendent of Int
        monsterDrops.Add(GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[34]);
        monsterDropChance.Add(1);


        //Arcana [fire, water, earth, air, life]
        arcanaDrop.Add(0);
        arcanaDrop.Add(2);
        arcanaDrop.Add(0);
        arcanaDrop.Add(1);
        arcanaDrop.Add(1);

        //experience drop
        if(!noXP)
        {
            experienceDrop = 10;
        }
        else
        {
            experienceDrop = 0;
        }
        

        StartCoroutine(MoveRoutine());
    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    IEnumerator MoveRoutine()
    {
        while(monsterHealth > 0)
        {

            //Check if the object is paused
            while (GameController.paused)
            {
                yield return null;
            }

            float distance = Vector3.Distance(transform.position, player.transform.position);
            
            /*
            //Determine what anim to play
            if (slimeAnim.GetCurrentAnimatorStateInfo(0).IsName("Slime_Idle"))
            {
                
            }
            else if (slimeAnim.GetCurrentAnimatorStateInfo(0).IsName("Slime_Idle_2"))
            {
                slimeAnim.Play("Slime_Jump_2");
            }
            */
            //Decide which direction to go
            
            //Random direction
            if(distance >= 1.75)
            {
                monsterRB.constraints = ~RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
                yield return new WaitForSeconds(Random.Range(1.5f, 3.5f));
                monsterRB.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
                int tempInt = Random.Range(1, 5);
                if(tempInt == 1)
                {
                    newDir = new Vector2(1, 0);
                    monsterRB.velocity = newDir;
                }
                else if(tempInt == 2)
                {
                    newDir = new Vector2(-1, 0);
                    monsterRB.velocity = newDir;
                }
                else if (tempInt == 3)
                {
                    newDir = new Vector2(0, 1);
                    monsterRB.velocity = newDir;
                }
                else if (tempInt == 4)
                {
                    newDir = new Vector2(0, -1);
                    monsterRB.velocity = newDir;
                }
            }
            //Towards player
            else
            {
                monsterRB.constraints = ~RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
                yield return new WaitForSeconds(1.5f);
                monsterRB.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
                newDir = (player.transform.position - transform.position).normalized;
                monsterRB.velocity = newDir;
            }

            float angle = Vector3.Angle(newDir, defaultDir);
            if(angle <= 90 || angle > 270)
            {
                slimeAnim.Play("Slime_Jump_2");
            }
            else
            {
                slimeAnim.Play("Slime_Jump_1");
            }

        }

    }
}
