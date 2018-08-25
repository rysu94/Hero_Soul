using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_Controller : Monster
{
    public GameObject childGoblin;
    public Animator goblinAnimator;

    GameObject currentNode, lastNode;

    public GameObject fearStatus;

    // Use this for initialization
    void Start ()
    {
        monsterName = "Goblin";

        monsterHealth = 70;
        contactDamage = 10;


        player = GameObject.FindGameObjectWithTag("Player");
        goblinAnimator = childGoblin.GetComponent<Animator>();
        monsterSprite = childGoblin.GetComponent<SpriteRenderer>();
        monsterRB = GetComponent<Rigidbody2D>();

        //Mushy Loot Table

        //Red Mushroom
        monsterDrops.Add(GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[7]);
        monsterDropChance.Add(250);
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
        arcanaDrop.Add(1);
        arcanaDrop.Add(0);
        arcanaDrop.Add(2);
        arcanaDrop.Add(1);
        arcanaDrop.Add(1);

        //experience drop
        experienceDrop = 15;


        StartCoroutine(GoblinRoutine());
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator GoblinRoutine()
    {
        //Start Anim
        int tempInt = Random.Range(0, 2);
        if(tempInt == 0)
        {
            goblinAnimator.Play("Goblin_Idle");
        }
        else
        {
            goblinAnimator.Play("Goblin_Idle_Right");
        }

        yield return new WaitForSeconds(Random.Range(1.5f, 3.5f));
        while(monsterHealth > 0)
        {
            //Move towards player
            GameObject target = player;
            //Find the player companion
            GameObject companion = GameObject.FindGameObjectWithTag("Companion");
            float distance = Vector2.Distance(player.transform.position, transform.position);

            if(companion!= null && Vector2.Distance(companion.transform.position, transform.position) < distance)
            {
                distance = Vector2.Distance(companion.transform.position, transform.position);
                target = companion;
            }
            
            if(distance > .5f && !isSliding)
            {
                //Path to the player
                GameObject node = LevelCreator.nodeGrid;
                GameObject originNode = LevelCreator.nodeGrid;
                GameObject enemyNode = LevelCreator.nodeGrid;
               
                int counter = 0;
                while (Vector2.Distance(transform.position, target.transform.position) > .5f && counter <= 50)
                {
                    GetComponent<CircleCollider2D>().enabled = false;
                    //Allows the enemy to move
                    monsterRB.constraints = ~RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
                    originNode = LevelCreator.nodeGrid.GetComponent<Path_Nodes>().FindClosestNode(transform.position, currentNode);
                    enemyNode = LevelCreator.nodeGrid.GetComponent<Path_Nodes>().FindClosestNode(target.transform.position, currentNode);
                    GetComponent<Rigidbody2D>().velocity = LevelCreator.nodeGrid.GetComponent<Path_Nodes>().Pathfind(originNode, enemyNode, gameObject, ref currentNode, ref lastNode) * .75f;
                    counter++;
                    //print(counter);
                    //Determine Animation to play
                    //Left
                    if (GetComponent<Rigidbody2D>().velocity.x < 0)
                    {
                        goblinAnimator.Play("Goblin_Run_Left");
                    }
                    //Right
                    else if (GetComponent<Rigidbody2D>().velocity.x > 0)
                    {
                        goblinAnimator.Play("Goblin_Run_Right");
                    }
                    yield return new WaitForSeconds(.1f);
                }
                GetComponent<CircleCollider2D>().enabled = true;
                //Determine Animation to play
                float dirVal = transform.position.x - target.transform.position.x;
                //Left
                if(dirVal >= 0)
                {
                    goblinAnimator.Play("Goblin_Run_Left");
                }
                //Right
                else
                {
                    goblinAnimator.Play("Goblin_Run_Right");
                }

            }
            else if(distance <= .5f && !isSliding)
            {
                /*
                
                //Determine Animation to play
                float dirVal = transform.position.x - target.transform.position.x;
                //Left
                if (dirVal >= 0)
                {
                    goblinAnimator.Play("Goblin_Idle");
                }
                //Right
                else
                {
                    goblinAnimator.Play("Goblin_Idle_Right");
                }
                */
                //Attack
                monsterRB.velocity = new Vector2(0, 0);
                //Determine Animation to play
                float dirVal2 = transform.position.x - target.transform.position.x;
                //Left
                if (dirVal2 >= 0)
                {
                    goblinAnimator.Play("Goblin_Left_Charge");
                }
                //Right
                else
                {
                    goblinAnimator.Play("Goblin_Right_Charge");
                }
                yield return new WaitForSeconds(.75f);
                //Determine Animation to play
                dirVal2 = transform.position.x - target.transform.position.x;
                //Left
                if (dirVal2 >= 0)
                {
                    goblinAnimator.Play("Goblin_Attack_Left");
                }
                //Right
                else
                {
                    goblinAnimator.Play("Goblin_Attack_Right");
                }
                AudioClip attack = Resources.Load("Sound/Cecilia/WeaponSwing1") as AudioClip;
                goblinAnimator.gameObject.GetComponent<AudioSource>().clip = attack;
                goblinAnimator.gameObject.GetComponent<AudioSource>().Play();
                yield return new WaitForSeconds(.6f);

                //Run away
                GameObject node = LevelCreator.nodeGrid;
                GameObject originNode = LevelCreator.nodeGrid;
                GameObject enemyNode = LevelCreator.nodeGrid;
                //Find a valid node
                List<GameObject> validNodes = new List<GameObject>();
                for(int i = 0; i < LevelCreator.nodeGrid.GetComponent<Path_Nodes>().nodeList.Count; i++)
                {
                    if(LevelCreator.nodeGrid.GetComponent<Path_Nodes>().nodeList[i].GetComponent<Node>().valid)
                    {
                        validNodes.Add(LevelCreator.nodeGrid.GetComponent<Path_Nodes>().nodeList[i]);
                    }
                }
                int randomNodeIndex = Random.Range(0, validNodes.Count);
                node = validNodes[randomNodeIndex];
                int counter = 0;
                fearStatus.SetActive(true);
                while (Vector2.Distance(node.transform.position, transform.position) > .25f && counter <= 50)
                {
                    GetComponent<CircleCollider2D>().enabled = false;
                    //Allows the enemy to move
                    monsterRB.constraints = ~RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
                    originNode = LevelCreator.nodeGrid.GetComponent<Path_Nodes>().FindClosestNode(transform.position, currentNode);
                    enemyNode = LevelCreator.nodeGrid.GetComponent<Path_Nodes>().FindClosestNode(node.transform.position, currentNode);
                    GetComponent<Rigidbody2D>().velocity = LevelCreator.nodeGrid.GetComponent<Path_Nodes>().Pathfind(originNode, node, gameObject, ref currentNode, ref lastNode) * .75f;
                    counter++;
                    //print(counter);
                    //Determine Animation to play
                    //Left
                    if (GetComponent<Rigidbody2D>().velocity.x < 0)
                    {
                        goblinAnimator.Play("Goblin_Run_Left");
                    }
                    //Right
                    else if (GetComponent<Rigidbody2D>().velocity.x > 0)
                    {
                        goblinAnimator.Play("Goblin_Run_Right");
                    }
                    yield return new WaitForSeconds(.1f);
                }
                fearStatus.SetActive(false);
                GetComponent<CircleCollider2D>().enabled = true;
                float dirVal = transform.position.x - target.transform.position.x;
                //Left
                if (dirVal >= 0)
                {
                    goblinAnimator.Play("Goblin_Idle");
                }
                //Right
                else
                {
                    goblinAnimator.Play("Goblin_Idle_Right");
                }
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                yield return new WaitForSeconds(Random.Range(1.5f, 2.5f));
            }
            else
            {
                monsterRB.velocity = new Vector2(0, 0);
                //Determine Animation to play
                float dirVal = transform.position.x - target.transform.position.x;
                //Left
                if (dirVal >= 0)
                {
                    goblinAnimator.Play("Goblin_Hurt_Left");
                }
                //Right
                else
                {
                    goblinAnimator.Play("Goblin_Hurt_Right");
                }
                yield return new WaitForSeconds(.5f);
                //Allows the enemy to move
                monsterRB.constraints = ~RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
            }
            yield return new WaitForSeconds(.1f);
        }
    }
}
