using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperController : Monster
{
    public Animator chomperAnim;

    public bool isOpen = false;

    public GameObject chomper_Attack;

    public int chompCounter;

	// Use this for initialization
	void Start ()
    {
        monsterName = "Chomper";

        monsterHealth = 100;
        contactDamage = 10;
        physicalResist = true;

        player = GameObject.FindGameObjectWithTag("Player");
        chomperAnim = GetComponent<Animator>();
        monsterSprite = GetComponent<SpriteRenderer>();
        monsterRB = GetComponent<Rigidbody2D>();

        //Arcana [fire, water, earth, air, life]
        arcanaDrop.Add(0);
        arcanaDrop.Add(1);
        arcanaDrop.Add(3);
        arcanaDrop.Add(0);
        arcanaDrop.Add(1);

        experienceDrop = 10;

        colNoise = GetComponent<AudioSource>().clip;

        StartCoroutine(StartBehavior());
        StartCoroutine(OpenMaw());
    }
	
	// Update is called once per frame
	void Update ()
    {

	}

    IEnumerator StartBehavior()
    {
        while(monsterHealth > 0)
        {
            //check if the monster is paused
            while(GameController.paused || frozen)
            {
                yield return null;
            }

            yield return new WaitForSeconds(2f);
            float distance = Vector2.Distance(transform.position, TestCharController.player.transform.position);
            if(distance < 1.5f)
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/Enemies/EnemyAttack/Chomper_Telegraph"), transform);
            }
        }
    }


    //Wait x Seconds, then open mouth. If mouth is ever open, close it in x seconds.
    IEnumerator OpenMaw()
    {
        while(monsterHealth > 0)
        {
            yield return new WaitForSeconds(1f);
            chompCounter++;
            if (isOpen || chompCounter >= 10)
            {
                chompCounter = 0;
                physicalResist = false;
                chomperAnim.Play("Chomper_Open");
                yield return new WaitForSeconds(5f);
                chomperAnim.Play("Chomper_Idle");
                isOpen = false;
                physicalResist = true;
            }
        }
    }


    public override void OnTriggerEnter2D(Collider2D collision)
    {
        //The normal trigger
        base.OnTriggerEnter2D(collision);

        //if monster is hit by a spell
        if(collision.gameObject.tag == "Spell_Destructible" || collision.gameObject.tag == "Spell_Multi" && !isOpen)
        {
            isOpen = true;
        }
    }

}
