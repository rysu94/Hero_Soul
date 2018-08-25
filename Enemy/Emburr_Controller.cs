using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emburr_Controller : Monster
{
    public Animator emburrAnim;
    public Vector2 defaultDir = new Vector2(1, 0);
    public Vector2 newDir;

    // Use this for initialization
    void Start ()
    {
        monsterName = "Emburr";

        monsterHealth = 155;
        contactDamage = 25;

        player = GameObject.FindGameObjectWithTag("Player");
        emburrAnim = GetComponent<Animator>();
        monsterSprite = GetComponent<SpriteRenderer>();
        monsterRB = GetComponent<Rigidbody2D>();

        //Unidentified Neck
        monsterDrops.Add(GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[80]);
        monsterDropChance.Add(10);


        //Arcana [fire, water, earth, air, life]
        arcanaDrop.Add(3);
        arcanaDrop.Add(0);
        arcanaDrop.Add(0);
        arcanaDrop.Add(0);
        arcanaDrop.Add(1);

        experienceDrop = 20;

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
            while (GameController.paused || frozen)
            {
                yield return null;
            }

            float distance = Vector3.Distance(transform.position, player.transform.position);

            //Random direction
            if (distance >= 1.75)
            {
                monsterRB.constraints = ~RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
                yield return new WaitForSeconds(Random.Range(1.5f, 3.5f));
                monsterRB.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
                int tempInt = Random.Range(1, 5);
                if (tempInt == 1)
                {
                    newDir = new Vector2(1, 0);
                    monsterRB.velocity = newDir * .25f;
                }
                else if (tempInt == 2)
                {
                    newDir = new Vector2(-1, 0);
                    monsterRB.velocity = newDir * .25f;
                }
                else if (tempInt == 3)
                {
                    newDir = new Vector2(0, 1);
                    monsterRB.velocity = newDir * .25f;
                }
                else if (tempInt == 4)
                {
                    newDir = new Vector2(0, -1);
                    monsterRB.velocity = newDir * .25f;
                }
            }
            //Towards player
            else
            {
                monsterRB.constraints = ~RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
                yield return new WaitForSeconds(1.5f);
                monsterRB.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
                newDir = (player.transform.position - transform.position).normalized * .25f;
                monsterRB.velocity = newDir;
            }

            float angle = Vector3.Angle(newDir, defaultDir);
            if (angle <= 90 || angle > 270)
            {
                emburrAnim.Play("Embur_Move2");
            }
            else
            {
                emburrAnim.Play("Embur_Move");
            }

            //Increase Size
            if (transform.localScale.x < 3)
            {
                transform.localScale = new Vector2(transform.localScale.x + .1f, transform.localScale.y + .1f);
                transform.Find("Embur_Light").GetComponent<Light>().cookieSize += .1f;
                contactDamage++;
            }

        }
    }
}
