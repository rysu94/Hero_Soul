using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Hero Project Monster Class
By: Ryan Su

The monster class is the class that all enemies will
inherit from.

*/

public class Monster : MonoBehaviour {

    //This is the player GameObject
    public GameObject player;

    //The distance from the monster to player
    public float distanceToPlayer;

    public Rigidbody2D monsterRB;

    //How much health the monster will have
    public int monsterHealth;

    //How much damage the player will take touching this monster
    public int contactDamage;

    //How much damage the player will take from an attack from this monster
    public int monsterDamage;

    //A flag determining whether the monster is dead
    public bool isDead;

    //These variables handle the color of the monster sprite when taking damage
    public SpriteRenderer monsterSprite;
    public float redValue = 0.3f;

    //This variables whether or not the monster is in the process of sliding
    public bool isSliding;

    //This variable flags whether or not the monster is in the process of collapsing
    public bool isCollapsing = false;

    //This variable flags whether or not the player is sliding
    public bool isPlayerSliding = false;

    //This variable flags whether or not there has been a collision
    public bool isTriggered = false;

    //The list which contains what items a monster will drop
    public List<Item> monsterDrops = new List<Item>();
    //The drop chances for each item in monster drops, same length as monsterDrops
    public List<int> monsterDropChance = new List<int>();
    //How much arcana monster will drop
   
    //[fire, water, earth, air, life]
    public List<int> arcanaDrop = new List<int>();

    //How much XP the monster will drop
    public int experienceDrop;

    //Invulnerable
    public bool invulnerable = false;

    public bool isActive = true;

    public bool physicalResist = false;

    public bool unstoppable = false;

    void Awake()
    {
        
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    
    //Deal X damage to the monster, and checks if the monster dies
    public void DamageMonster(int damage)
    {
        monsterHealth -= damage;

        if(monsterHealth <= 0 && !isCollapsing)
        {
            StartCoroutine("DeathRoutine");

            //Plays the Collapse Noise from the Mushy
            AudioSource deathNoise = GetComponent<AudioSource>();
            deathNoise.Play();
        } 
    }

    //=================================================================================
    //                            Monster Coroutine Suite
    //
    //=================================================================================

    //Contains the Collapse routine of a Monster
    IEnumerator DeathRoutine()
    {
        //Check if the object is paused
        while (GameController.paused)
        {
            monsterRB.velocity = new Vector2(0, 0);
            yield return null;
        }


        //Set the collapsing flag to true
        isCollapsing = true;

        //Ends the on-hit color change coroutine and return enemy back to base color
        StopCoroutine("ColorReturnRoutineEnemy");
        player.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);

        //Disables the box collider the enemy has
        CircleCollider2D enemyHitbox = GetComponent<CircleCollider2D>();
        enemyHitbox.enabled = false;

        //If player is hit during the collapse, returns them back to normal
        TestCharController.isHit = false;

        //Fades out enemy over time
        float tempColor = 1;
        while (tempColor >= 0)
        {
            yield return new WaitForSeconds(.05f);
            tempColor -= .1f;
            monsterSprite.color = new Color(1f, 1f, 1f, tempColor);
        }

        //Updates the enemy count in the current room, if there are no more enemies open doors
        LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].enemyCount--;
        print("Enemy ded" + LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].enemyCount);
        if (LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].enemyCount <= 0)
        {
            LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].roomClear = true;
            AudioSource tempAudio = GameObject.Find("RoomDone").GetComponent<AudioSource>();
            tempAudio.Play();
        }

        //Process Monster's Loot Drops
        ItemDrop();

        //Player Gain XP
        PlayerStats.GainXP(experienceDrop);

        //Disables the enemy
        gameObject.SetActive(false);
    }

    //This coroutine executes a enemy slide when damaged
    IEnumerator EnemySlideRoutine()
    {
        isSliding = true;
        
       
        //get the direction

        if(!unstoppable)
        {
            //Allows the enemy to move
            monsterRB.constraints = ~RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
            //Allows the enemy to move
            Vector2 direction = (player.transform.position - transform.position).normalized;
            monsterRB.velocity = -direction * .25f;
        }


        yield return new WaitForSeconds(.1f);

        if(!unstoppable)
        {
            //Restricts enemy movements
            monsterRB.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
        }

        isSliding = false;
        isTriggered = false;
    }

    //This coroutine executes a player slide when contacting an enemy
    IEnumerator PlayerSlideRoutine()
    {
        isPlayerSliding = true;

        //This flag prevents players from moving when they are getting hit
        TestCharController.isHit = true;

        //Determines the vector for the player to get knocked back
        var direction = (player.transform.position - transform.position).normalized;

        if(unstoppable)
        {
            player.GetComponent<Rigidbody2D>().velocity = direction * 2;
            //StartCoroutine(FlickerHitbox());
        }
        else
        {
            player.GetComponent<Rigidbody2D>().velocity = direction * 2;
        }
        

        //Deal the monster's contact damage to the player
        DamageManager.PlayerDamage(contactDamage, player, false);

        //Play the player's getting hurt SFX
        if(!TestCharController.breakInvuln)
        {
            TestCharController.playerAttack.clip = TestCharController.hurtNoise[Random.Range(0, TestCharController.hurtNoise.Length)];
            TestCharController.playerAttack.Play();
        }

        yield return new WaitForSeconds(.15f);

        TestCharController.isHit = false;

        isPlayerSliding = false;
    }

    IEnumerator FlickerHitbox()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(1f);
        GetComponent<CircleCollider2D>().enabled = true;
    }

    //This couroutine changes the player's sprite color temporarily to red when damaged
    IEnumerator ColorReturnRoutinePlayer()
    {
        float tempColor = redValue;
        while (tempColor <= 1)
        {
            yield return new WaitForSeconds(.05f);
            tempColor += .1f;
            player.GetComponent<SpriteRenderer>().color = new Color(1f, tempColor, tempColor);
        }
    }



    //This Coroutine change's the monster's sprite color to red when damaged
    IEnumerator ColorReturnRoutineEnemy()
    {
        float tempColor = redValue;
        while (tempColor <= 1)
        {
            yield return new WaitForSeconds(.05f);
            tempColor += .1f;
            monsterSprite.color = new Color(1f, tempColor, tempColor);
        }
    }

    //Deals with Multiple Instances of Damage
    IEnumerator MultiDamage()
    {
        float startOffset = -.025f;
        float offsetIncrement = .075f / DamageManager.totalHits;
        
        for (int i = 0; i < DamageManager.totalHits; i++)
        {
            int spellDamage = DamageManager.MagicDamage(startOffset, this.gameObject);
            startOffset += offsetIncrement;
            DamageMonster(spellDamage);
            yield return new WaitForSeconds(.05f);
        }

        DamageManager.totalHits = 0;
    }

    //=================================================================================
    //                            Monster Collision
    //
    //=================================================================================

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Weapon" && !isTriggered)
        {
            isTriggered = true;
            
            if(physicalResist || invulnerable)
            {
                int wepDamage = DamageManager.WeaponDamage(this.gameObject);
                DamageMonster(0);
            }
            else
            {
                int wepDamage = DamageManager.WeaponDamage(this.gameObject);
                DamageMonster(wepDamage);
            }
            

            if (!isCollapsing)
            {
                monsterSprite.color = new Color(1f, redValue, redValue);
                StartCoroutine("ColorReturnRoutineEnemy");
            }

            if (!isSliding && !isCollapsing)
            {
                StartCoroutine("EnemySlideRoutine");
            }

            if(TestCharController.player.GetComponent<TestCharController>().isSliding)
            {
                TestCharController.player.GetComponent<TestCharController>().playerRB.velocity = new Vector2(0, 0);
            }

            if(TestCharController.startBreak)
            {
                print("hit");
                TestCharController.player.GetComponent<TestCharController>().playerRB.velocity = new Vector2(0, 0);
                TestCharController.breakHit = true;
                TestCharController.player.GetComponent<TestCharController>().breakTargetSingle = gameObject;
            }

        }
        else if (collision.gameObject.tag == "Spell_Destructible" && !isTriggered && !invulnerable)
        {
            isTriggered = true;
            int spellDamage = DamageManager.MagicDamage(0, this.gameObject);
            DamageMonster(spellDamage);

            if (!isCollapsing)
            {
                monsterSprite.color = new Color(1f, redValue, redValue);
                StartCoroutine("ColorReturnRoutineEnemy");
            }
            if (!isSliding && !isCollapsing)
            {
                StartCoroutine("EnemySlideRoutine");
            }
        }

        else if (collision.gameObject.tag == "Spell_Multi" && !isTriggered && !invulnerable)
        {
            isTriggered = true;
            StartCoroutine("MultiDamage");

            if (!isCollapsing)
            {
                monsterSprite.color = new Color(1f, redValue, redValue);
                StartCoroutine("ColorReturnRoutineEnemy");
            }
            if (!isSliding && !isCollapsing)
            {
                StartCoroutine("EnemySlideRoutine");
            }
        }

        else if (collision.gameObject.tag == "Player")
        {

            if (!isCollapsing && !TestCharController.invuln)
            {
                player.GetComponent<SpriteRenderer>().color = new Color(1f, redValue, redValue);
                StartCoroutine("ColorReturnRoutinePlayer");
            }

            if (!isPlayerSliding && !isCollapsing && !TestCharController.breakInvuln)
            {
                StartCoroutine("PlayerSlideRoutine");

                if(!unstoppable)
                {
                    StartCoroutine("EnemySlideRoutine");
                }
                
            }
        }
    }
    //=================================================================================
    //                            Monster Drops
    //
    //=================================================================================

    void ItemDrop()
    {
        //check if the monster can drop anything
        if(monsterDrops.Count > 0)
        {
            int dropChance = 0;
            for(int i = 0; i < monsterDrops.Count; i++)
            {
                dropChance = Random.Range(1, 101);
                if(dropChance <= monsterDropChance[i])
                {
                    GameObject drop = Instantiate(Resources.Load("Prefabs/Items/" + monsterDrops[i].itemIconName),
                        new Vector2(transform.position.x, transform.position.y), Quaternion.identity) as GameObject;

                    Rigidbody2D dropRB = drop.GetComponent<Rigidbody2D>();
                    dropRB.velocity = new Vector2(Random.Range(-5f, 5f), Random.Range(-.5f, .5f)).normalized * Random.Range(0,.5f);

                }
            }
        }

        //drop arcana
        
        //Fire
        if(arcanaDrop[0] > 0)
        {
            for(int i = 0; i < arcanaDrop[0]; i++)
            {
                GameObject drop = Instantiate(Resources.Load("Prefabs/Arcana/Ar_Fire"),
                        new Vector2(transform.position.x, transform.position.y), Quaternion.identity) as GameObject;
                Rigidbody2D dropRB = drop.GetComponent<Rigidbody2D>();
                dropRB.velocity = new Vector2(Random.Range(-5f, .5f), Random.Range(-.5f, .5f)).normalized * Random.Range(0, .5f);
            }            
        }

        //Water
        if (arcanaDrop[1] > 0)
        {
            for (int i = 0; i < arcanaDrop[1]; i++)
            {
                GameObject drop = Instantiate(Resources.Load("Prefabs/Arcana/Ar_Water"),
                        new Vector2(transform.position.x, transform.position.y), Quaternion.identity) as GameObject;
                Rigidbody2D dropRB = drop.GetComponent<Rigidbody2D>();
                dropRB.velocity = new Vector2(Random.Range(-5f, 5f), Random.Range(-.5f, .5f)).normalized * Random.Range(0, .5f);
            }
        }

        //Earth
        if (arcanaDrop[2] > 0)
        {
            for (int i = 0; i < arcanaDrop[2]; i++)
            {
                GameObject drop = Instantiate(Resources.Load("Prefabs/Arcana/Ar_Earth"),
                        new Vector2(transform.position.x, transform.position.y), Quaternion.identity) as GameObject;
                Rigidbody2D dropRB = drop.GetComponent<Rigidbody2D>();
                dropRB.velocity = new Vector2(Random.Range(-5, 5f), Random.Range(-.5f, .5f)).normalized * Random.Range(0, .5f);
            }
        }

        //Wind
        if (arcanaDrop[3] > 0)
        {
            for (int i = 0; i < arcanaDrop[3]; i++)
            {
                GameObject drop = Instantiate(Resources.Load("Prefabs/Arcana/Ar_Air"),
                        new Vector2(transform.position.x, transform.position.y), Quaternion.identity) as GameObject;
                Rigidbody2D dropRB = drop.GetComponent<Rigidbody2D>();
                dropRB.velocity = new Vector2(Random.Range(-.5f, .5f), Random.Range(-.5f, .5f)).normalized * Random.Range(0, .5f);
            }
        }

        //Life
        if (arcanaDrop[4] > 0)
        {
            for (int i = 0; i < arcanaDrop[4]; i++)
            {
                GameObject drop = Instantiate(Resources.Load("Prefabs/Arcana/Ar_Life"),
                        new Vector2(transform.position.x, transform.position.y), Quaternion.identity) as GameObject;
                Rigidbody2D dropRB = drop.GetComponent<Rigidbody2D>();
                dropRB.velocity = new Vector2(Random.Range(-5f, 5f), Random.Range(-.5f, .5f)).normalized * Random.Range(0, .5f);
            }

        }


    }


}
