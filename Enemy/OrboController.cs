using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrboController : Monster
{
    public Animator orboAnim;

    public Vector2 defaultDir = new Vector2(1, 0);
    public Vector2 newDir;

    public int jumpCount = 0;
    public int missileCount = 0;

    public GameObject orboMissile;

    // Use this for initialization
    void Start ()
    {
        monsterName = "Orbo";

        monsterHealth = 65;
        contactDamage = 10;

        player = GameObject.FindGameObjectWithTag("Player");
        orboAnim = GetComponent<Animator>();
        monsterSprite = GetComponent<SpriteRenderer>();
        monsterRB = GetComponent<Rigidbody2D>();

        //Orbo Loot Table

        //Hard Carapace
        monsterDrops.Add(GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[37]);
        monsterDropChance.Add(333);

        //Arcana [fire, water, earth, air, life]
        arcanaDrop.Add(0);
        arcanaDrop.Add(0);
        arcanaDrop.Add(2);
        arcanaDrop.Add(0);
        arcanaDrop.Add(1);

        experienceDrop = 10;

        colNoise = GetComponent<AudioSource>().clip;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (isActive)
        {
            StartCoroutine(MoveRoutine());
            isActive = false;
        }
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

            if (jumpCount < 4)
            {
                monsterRB.constraints = ~RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
                yield return new WaitForSeconds(Random.Range(1f, 3.5f));
                monsterRB.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
                monsterRB.velocity = new Vector2(Random.Range(-5, 6), Random.Range(-5, 6)).normalized;
                jumpCount++;
            }
            else
            {
                orboAnim.Play("Orbo_Attack");
                yield return new WaitForSeconds(1f);
                orboAnim.Play("Orbo_Attack_Hold");
                for(int i = 0; i < 10; i++)
                {

                    //Check if the object is paused
                    while (GameController.paused || frozen)
                    {
                        yield return null;
                    }
                    missileCount++;
                    if(i%4 == 0)
                    {
                        Instantiate(orboMissile, transform.position, Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/Orbo_Missile_2"), transform.position, Quaternion.identity);
                    }
                    
                    yield return new WaitForSeconds(1f);
                }
                orboAnim.Play("Orbo_Attack_Close");
                yield return new WaitForSeconds(1f);
                jumpCount = 0;
            }


        }
    }
}
