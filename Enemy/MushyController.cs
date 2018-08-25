using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Hero Project Mushy Class
By: Ryan Su

This class holds the unique behaviors of the mushy enemy, including animations and AI

*/

public class MushyController : Monster
{
    //Mushy animation controller
    public Animator mushyController;

    public bool mushyAwake = false;
    public bool isChecking = false;

    public Coroutine attackSeq;
    public bool isAttacking = false;

    public int angle = 0;

	// Use this for initialization
	void Start ()
    {
        monsterName = "Mushy";

        monsterHealth = 50;
        contactDamage = 10;


        player = GameObject.FindGameObjectWithTag("Player");
        mushyController = GetComponent<Animator>();
        monsterSprite = GetComponent<SpriteRenderer>();
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
        arcanaDrop.Add(0);
        arcanaDrop.Add(0);
        arcanaDrop.Add(3);
        arcanaDrop.Add(0);
        arcanaDrop.Add(1);

        //experience drop
        experienceDrop = 15;

        colNoise = GetComponent<AudioSource>().clip;
    }
	
	// Update is called once per frame
	void Update ()
    {

        //Check if enemies are paused
        if(!GameController.paused && !frozen)
        {
            if (!isChecking)
            {
                StartCoroutine(MushyAnimCheckRoutine());
            }

            if (mushyAwake && !isAttacking)
            {
                attackSeq = StartCoroutine(AttackRoutine());
            }

            else if (!mushyAwake)
            {
                if (isAttacking)
                {
                    StopCoroutine(attackSeq);
                }
                isAttacking = false;
            }
        }


	}

    //The animation behavior of the Mushy
    IEnumerator MushyAnimCheckRoutine()
    {
        isChecking = true;
        yield return new WaitForSeconds(0.5f);
        
        distanceToPlayer = Vector3.Distance(player.transform.position, this.transform.position);
        if (!mushyAwake && distanceToPlayer > 1.5f)
        {
            mushyController.Play("Mushy_Idle");
        }
        else if (mushyAwake && distanceToPlayer > 1.5f)
        {
            mushyController.Play("Mushy_Sleep");
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
            mushyAwake = false;
        }
        else if (!mushyAwake && distanceToPlayer < 1.5f)
        {
            mushyController.Play("Mushy_Awake_Down");
            mushyAwake = true;
        }
        isChecking = false;
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;

        //Check if the object is paused
        while(GameController.paused)
        {
            yield return null;
        }


        for(float i = 1f; i > 0; i -= .1f)
        {

            //Check if the object is paused
            while (GameController.paused)
            {
                yield return null;
            }

            yield return new WaitForSeconds(.1f);
            GetComponent<SpriteRenderer>().color = new Color(1f, i, 1f);
        }
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
        Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/Spore_Cloud"), transform.position, Quaternion.identity);
        Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/Spore_Mushy"), transform.position, Quaternion.Euler(0,0,angle));
        angle += 15;
        //Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/Spore_Mushy"), transform.position, Quaternion.Euler(0,0,15));
        //Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/Spore_Mushy"), transform.position, Quaternion.Euler(0, 0, 30));
        yield return new WaitForSeconds(3f);
        isAttacking = false;
    }


}
