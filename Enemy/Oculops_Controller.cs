using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oculops_Controller : Monster
{
    public bool left;
    public bool attacking;

    public Vector2 originPoint;

	// Use this for initialization
	void Start ()
    {
        monsterName = "Oculops";

        monsterHealth = 60;
        contactDamage = 10;

        player = GameObject.FindGameObjectWithTag("Player");
        monsterSprite = GetComponent<SpriteRenderer>();
        monsterRB = GetComponent<Rigidbody2D>();

        //Oculops Loot Table

        //Mighty Copper Ring
        monsterDrops.Add(GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[11]);
        monsterDropChance.Add(10);
        //Nimble Copper Ring
        monsterDrops.Add(GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[12]);
        monsterDropChance.Add(10);
        //Pendent of Might
        monsterDrops.Add(GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[31]);
        monsterDropChance.Add(10);
        //Pendent of Agility
        monsterDrops.Add(GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[32]);
        monsterDropChance.Add(10);

        //Arcana [fire, water, earth, air, life]
        arcanaDrop.Add(0);
        arcanaDrop.Add(0);
        arcanaDrop.Add(2);
        arcanaDrop.Add(1);
        arcanaDrop.Add(1);

        //experience drop
        experienceDrop = 15;

        colNoise = GetComponent<AudioSource>().clip;
        unstoppable = true;

        originPoint = transform.position;

        StartCoroutine(OculopsRoutine());
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator OculopsRoutine()
    {
        //Determine Start Direction
        int tempInt = Random.Range(0, 2);
        if (tempInt == 0)
        {
            GetComponent<Animator>().Play("Oculops_Idle");
            left = true;
        }
        else if (tempInt == 1)
        {
            GetComponent<Animator>().Play("Oculops_Idle_2");
            left = false;
        }

        while (monsterHealth > 0)
        {
            if(left)
            {
                GetComponent<Animator>().Play("Oculops_Idle");
            }
            else
            {
                GetComponent<Animator>().Play("Oculops_Idle_2");
            }


            for(int i = 0; i < 50; i++)
            {
                //Check if the player is within the eyes line of sight
                if(player.transform.position.y <= transform.position.y + .05f && player.transform.position.y >= transform.position.y - .05f)
                {
                    if(left && player.transform.position.x < transform.position.x)
                    {
                        //Attack sequence
                        attacking = true;
                        break;
                    }
                    else if(!left && player.transform.position.x > transform.position.x)
                    {
                        //Attack sequence
                        attacking = true;
                        break;
                    }
                }
                yield return new WaitForSeconds(Random.Range(.01f, .15f));
            }
            attacking = true;

            if(attacking)
            {
                originPoint = transform.position;
                StartCoroutine(StopChecker());
                if(left)
                {
                    GetComponent<Animator>().Play("Oculops_Attack");                   
                }
                else
                {
                    GetComponent<Animator>().Play("Oculops_Attack_2");
                }
                yield return new WaitForSeconds(.5f);
            }


            //oculops attack sequence
            while(attacking)
            {
                if(left)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(-2, 0);
                }
                else
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(2, 0);
                }

                yield return new WaitForSeconds(.1f);

                if ((Vector2)transform.position == originPoint)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                    break;
                }
                
                
            }
            

            if(left)
            {
                GetComponent<Animator>().Play("Oculops_Turn");
                left = false;
            }
            else
            {
                GetComponent<Animator>().Play("Oculops_Turn_2");
                left = true;
            }
            yield return new WaitForSeconds(.5f);
        }
    }

    //When the Ocuclops becomes invisible
    private void OnBecameInvisible()
    {
        Vector2 pos = transform.position;
        Vector2 vel = GetComponent<Rigidbody2D>().velocity.normalized;

        float newX = vel.x * 8;
        float newY = vel.y * 8;

        pos = new Vector2(pos.x - newX, pos.y - newY);

        transform.position = pos;
    }

    IEnumerator StopChecker()
    {
        yield return new WaitForSeconds(1f);
        while(attacking)
        {
            if(Vector2.Distance(transform.position, originPoint) < .1f)
            {              
                attacking = false;
            }
            yield return new WaitForEndOfFrame();
        }


    }

}
