using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkCloud_Controller : Monster
{
    float xVel = 0;
    float yVel = 0;

    int internalTimer = 0;
    int internalTimerMax;

    // Use this for initialization
    void Start ()
    {
        monsterName = "Shadowy Cloud";

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
        arcanaDrop.Add(0);
        arcanaDrop.Add(0);
        arcanaDrop.Add(0);

        experienceDrop = 10;

        //Set batty's inital velocity
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

        invulnerable = true;
        unstoppable = true;

        internalTimer = 0;
        internalTimerMax = Random.Range(10, 25);

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
            if(internalTimer < internalTimerMax)
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
                    xVel = Random.Range(.1f, .25f);
                }
                else if (transform.position.x > 2.35)
                {
                    xVel = -Random.Range(.1f, .25f);
                }

                //Check up/down
                if (transform.position.y < -1.3)
                {
                    yVel = Random.Range(.1f, .25f);
                }
                else if (transform.position.y > 1.15)
                {
                    yVel = -Random.Range(.1f, .25f);
                }

                monsterRB.constraints = ~RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
                if (!isSliding)
                {
                    monsterRB.velocity = new Vector2(xVel, yVel);
                }
                internalTimer++;
                yield return new WaitForSeconds(.75f);
                //Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/Shadow_Puddle"), transform.position, Quaternion.identity);
            }
            else
            {
                monsterRB.velocity = new Vector2(0, 0);
                internalTimer = 0;
                internalTimerMax = Random.Range(10, 25);

                float[] spikeCoords = new float[4];
                spikeCoords[0] = transform.position.x;
                spikeCoords[1] = transform.position.x;
                spikeCoords[2] = transform.position.y;
                spikeCoords[3] = transform.position.y;

                //Create Telegraph
                GameObject tempObj1 = Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/Cloud_Telegraph_Vert"), new Vector2(transform.position.x, -0.868f), Quaternion.Euler(new Vector3(0,0,90))) as GameObject;
                GameObject tempObj2 = Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/Cloud_Telegraph_Hori"), new Vector2(-1.261f, transform.position.y), Quaternion.identity) as GameObject;

                yield return new WaitForSeconds(3f);

                Destroy(tempObj1);
                Destroy(tempObj2);

                //Start Shooting
                while(spikeCoords[0] > -2.416f || spikeCoords[1] < 2.416f || spikeCoords[2] < 1.219f || spikeCoords[3] > -1.53f)
                {
                    //West        
                    if(spikeCoords[0] > -2.416f)
                    {
                        Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/DarkCloud_Spike"), new Vector2(spikeCoords[0], transform.position.y), Quaternion.identity);
                        spikeCoords[0] -= .25f;
                    }
                    //East                  
                    if (spikeCoords[1] < 2.416f)
                    {
                        Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/DarkCloud_Spike"), new Vector2(spikeCoords[1], transform.position.y), Quaternion.identity);
                        spikeCoords[1] += .25f;
                    }
                    //North
                    if (spikeCoords[2] < 1.219f)
                    {
                        Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/DarkCloud_Spike"), new Vector2(transform.position.x, spikeCoords[2]), Quaternion.identity);
                        spikeCoords[2] += .25f;
                    }
                    //South
                    if (spikeCoords[3] > -1.53f)
                    {
                        Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/DarkCloud_Spike"), new Vector2(transform.position.x, spikeCoords[3]), Quaternion.identity);
                        spikeCoords[3] -= .25f;
                    }

                    yield return new WaitForSeconds(.15f);
                }

            }

        }
    }
}
