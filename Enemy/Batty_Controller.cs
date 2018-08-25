using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batty_Controller : Monster
{
    float xVel = 0;
    float yVel = 0;


    // Use this for initialization
    void Start ()
    {
        monsterName = "Batty";

        monsterHealth = 145;
        contactDamage = 25;

        player = GameObject.FindGameObjectWithTag("Player");
        monsterSprite = GetComponent<SpriteRenderer>();
        monsterRB = GetComponent<Rigidbody2D>();

        //Hard Carapace
        monsterDrops.Add(GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[37]);
        monsterDropChance.Add(333);

        //Arcana [fire, water, earth, air, life]
        arcanaDrop.Add(0);
        arcanaDrop.Add(0);
        arcanaDrop.Add(1);
        arcanaDrop.Add(2);
        arcanaDrop.Add(1);

        experienceDrop = 10;

        //Set batty's inital velocity
        int tempInt = Random.Range(1, 3);
        if(tempInt == 1)
        {
            xVel = .5f;
        }
        else
        {
            xVel = -.5f;
        }

        tempInt = Random.Range(1, 3);
        if(tempInt == 1)
        {
            yVel = .5f;
        }
        else
        {
            yVel = -.5f;
        }

        colNoise = GetComponent<AudioSource>().clip;

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

            //check if the monster is paused
            while (GameController.paused || frozen)
            {
                monsterRB.velocity = new Vector2(0, 0);
                yield return null;
            }

            //Check left/right
            if (transform.position.x < -2.35)
            {
                xVel = .5f;
            }
            else if(transform.position.x > 2.35)
            {
                xVel = -.5f;
            }

            //Check up/down
            if(transform.position.y < -1.3)
            {
                yVel = .5f;
            }
            else if(transform.position.y > 1.3)
            {
                yVel = -.5f;
            }

            monsterRB.constraints = ~RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
            if(!isSliding)
            {
                monsterRB.velocity = new Vector2(xVel, yVel);
            }
            
            yield return new WaitForSeconds(.5f);
        }
    }
}
