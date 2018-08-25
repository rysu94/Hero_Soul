using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruct_Scatter : MonoBehaviour
{
    public List<GameObject> shards = new List<GameObject>();

    public int hitPoints;

    bool isTriggered = false;
	// Use this for initialization
	void Start ()
    {
		//Is the destructible broken?
        if(hitPoints <= 0)
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Weapon" && !isTriggered)
        {
            if(hitPoints > 0)
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/Hit/Wep_Hit_1"), transform.position, Quaternion.identity);
                StartCoroutine(BufferShaker());
            }
            else
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/Hit/Wep_Hit_1"), transform.position, Quaternion.identity);
                GetComponent<BoxCollider2D>().enabled = false;

                //Find Player
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                for (int i = 0; i < shards.Count; i++)
                {
                    Vector2 dir = (transform.position - player.transform.position).normalized;
                    shards[i].GetComponent<Rigidbody2D>().velocity = new Vector2(dir.x * Random.Range(.25f, 1.25f), dir.y * Random.Range(.15f, 2f));
                    shards[i].GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-90f, 90f);
                    shards[i].GetComponent<SpriteRenderer>().sortingOrder = 2;
                }
            }

        }
    }

    IEnumerator BufferShaker()
    {
        isTriggered = true;
        Vector2 originPos = transform.position;

        for(int i = 0; i < 3; i++)
        {
            transform.position = originPos + new Vector2(Random.Range(-.01f, .01f), 0);
            yield return new WaitForSeconds(.1f);
        }
        hitPoints--;
        transform.position = originPos;
        isTriggered = false;
    }
}
