using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pebble_Controller : Monster
{

    public Animator pebbleAnim;

    float xVel = 0;
    float yVel = 0;

    // Use this for initialization
    void Start ()
    {
        monsterName = "Pebble";

        monsterHealth = 150;
        contactDamage = 15;

        player = GameObject.FindGameObjectWithTag("Player");
        pebbleAnim = GetComponent<Animator>();
        monsterSprite = GetComponent<SpriteRenderer>();
        monsterRB = GetComponent<Rigidbody2D>();

        //Arcane Copper Ring
        monsterDrops.Add(GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[13]);
        monsterDropChance.Add(10);
        //Enduring Copper Ring
        monsterDrops.Add(GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[14]);
        monsterDropChance.Add(10);
        //Pendent of Endurance
        monsterDrops.Add(GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[33]);
        monsterDropChance.Add(10);
        //Pendent of Int
        monsterDrops.Add(GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[34]);
        monsterDropChance.Add(10);

        //Arcana [fire, water, earth, air, life]
        arcanaDrop.Add(0);
        arcanaDrop.Add(0);
        arcanaDrop.Add(1);
        arcanaDrop.Add(0);
        arcanaDrop.Add(0);

        int tempInt = Random.Range(1, 3);
        if (tempInt == 1)
        {
            xVel = .15f;
        }
        else
        {
            xVel = -.15f;
        }

        tempInt = Random.Range(1, 3);
        if (tempInt == 1)
        {
            yVel = .15f;
        }
        else
        {
            yVel = -.15f;
        }

        experienceDrop = 0;

        StartCoroutine(MoveRoutine());
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator MoveRoutine()
    {
        while (monsterHealth > 0)
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
                xVel = .15f;
            }
            else if (transform.position.x > 2.35)
            {
                xVel = -.15f;
            }

            //Check up/down
            if (transform.position.y < -1.3)
            {
                yVel = .15f;
            }
            else if (transform.position.y > 1.3)
            {
                yVel = -.15f;
            }

            monsterRB.constraints = ~RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
            if (!isSliding)
            {
                monsterRB.velocity = new Vector2(xVel, yVel);
            }

            yield return new WaitForSeconds(.5f);
        }
    }
}
