using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerPod : MonoBehaviour
{
    public GameObject sporeCloud;
    public bool triggered = false;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(!triggered && collision.gameObject.tag == "Weapon")
        {
            int tempInt = Random.Range(1, 10);

            GetComponent<Animator>().Play("ForestPod");
            sporeCloud.SetActive(true);
            triggered = true;
            if(tempInt == 1 || tempInt > 5)
            {
                Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/Spore_Mushy"), transform.position, Quaternion.Euler(0, 0, 0));
            }
            else if (tempInt == 2)
            {
                GameObject drop = Instantiate(Resources.Load("Prefabs/Arcana/Ar_Life"),
                    new Vector2(transform.position.x, transform.position.y), Quaternion.identity) as GameObject;
                Rigidbody2D dropRB = drop.GetComponent<Rigidbody2D>();
                dropRB.velocity = new Vector2(Random.Range(-5f, 5f), Random.Range(-.5f, .5f)).normalized * Random.Range(0, .5f);
            }
            else if (tempInt == 3)
            {
                GameObject drop = Instantiate(Resources.Load("Prefabs/Arcana/Ar_Air"),
                    new Vector2(transform.position.x, transform.position.y), Quaternion.identity) as GameObject;
                Rigidbody2D dropRB = drop.GetComponent<Rigidbody2D>();
                dropRB.velocity = new Vector2(Random.Range(-5f, 5f), Random.Range(-.5f, .5f)).normalized * Random.Range(0, .5f);
            }
            else if (tempInt == 4)
            {
                GameObject drop = Instantiate(Resources.Load("Prefabs/Arcana/Ar_Earth"),
                    new Vector2(transform.position.x, transform.position.y), Quaternion.identity) as GameObject;
                Rigidbody2D dropRB = drop.GetComponent<Rigidbody2D>();
                dropRB.velocity = new Vector2(Random.Range(-5f, 5f), Random.Range(-.5f, .5f)).normalized * Random.Range(0, .5f);
            }
            else if (tempInt == 5)
            {
                GameObject drop = Instantiate(Resources.Load("Prefabs/Arcana/Ar_Water"),
                    new Vector2(transform.position.x, transform.position.y), Quaternion.identity) as GameObject;
                Rigidbody2D dropRB = drop.GetComponent<Rigidbody2D>();
                dropRB.velocity = new Vector2(Random.Range(-5f, 5f), Random.Range(-.5f, .5f)).normalized * Random.Range(0, .5f);
            }
            else if (tempInt == 5)
            {
                GameObject drop = Instantiate(Resources.Load("Prefabs/Arcana/Ar_Fire"),
                    new Vector2(transform.position.x, transform.position.y), Quaternion.identity) as GameObject;
                Rigidbody2D dropRB = drop.GetComponent<Rigidbody2D>();
                dropRB.velocity = new Vector2(Random.Range(-5f, 5f), Random.Range(-.5f, .5f)).normalized * Random.Range(0, .5f);
            }



        }
        
    }
}
