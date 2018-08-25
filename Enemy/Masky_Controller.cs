using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Masky_Controller : Monster
{

    public int shotTimer = 0;

    public GameObject maskyMissile;

	// Use this for initialization
	void Start ()
    {
        monsterName = "Masky";

        monsterHealth = 80;
        contactDamage = 15;

        player = GameObject.FindGameObjectWithTag("Player");
        monsterSprite = GetComponent<SpriteRenderer>();
        monsterRB = GetComponent<Rigidbody2D>();

        //Hard Carapace
        monsterDrops.Add(GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[37]);
        monsterDropChance.Add(333);

        //Arcana [fire, water, earth, air, life]
        arcanaDrop.Add(0);
        arcanaDrop.Add(0);
        arcanaDrop.Add(3);
        arcanaDrop.Add(0);
        arcanaDrop.Add(1);

        experienceDrop = 10;

        hitNoise = Resources.Load("Sound/MaskyHit") as AudioClip;

        int tempInt = Random.Range(1, 3);
        if(tempInt == 1)
        {
            transform.position = new Vector2(Random.Range(-2.2f, -.6f), 1.2f);
        }
        else
        {
            transform.position = new Vector2(Random.Range(.6f, 2.2f), 1.2f);
        }

        colNoise = GetComponent<AudioSource>().clip;

        StartCoroutine(AttackRoutine());

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator AttackRoutine()
    {
        while(monsterHealth > 0)
        {

            //Check if the object is paused
            while (GameController.paused || frozen)
            {
                yield return null;
            }

            float greenColor = 1f;
            float blueColor = 1f;
            for (int i = 0; i < 10; i++)
            {

                //Check if the object is paused
                while (GameController.paused || frozen)
                {
                    yield return null;
                }

                monsterRB.velocity = new Vector2(Random.Range(-1, 2), 0).normalized;
                yield return new WaitForSeconds(.5f);
                greenColor -= .05f;
                blueColor -= .05f;
                monsterSprite.color = new Color(1f, greenColor, blueColor);
            }
            yield return new WaitForSeconds(1f);
            monsterSprite.color = new Color(1f, 1f, 1f);
            for (int i = 0; i < 5; i++)
            {

                //Check if the object is paused
                while (GameController.paused)
                {
                    yield return null;
                }

                Instantiate(maskyMissile, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(.5f);
            }
        }
    }

}
