using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpin : MonoBehaviour
{

    public int decayTime = 7;
    

	// Use this for initialization
	void Start ()
    {
        if (TestCharController.player.GetComponent<TestCharController>().north)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1);
        }
        else if (TestCharController.player.GetComponent<TestCharController>().south)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1);
        }
        else if (TestCharController.player.GetComponent<TestCharController>().east)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0);
        }
        else if (TestCharController.player.GetComponent<TestCharController>().west)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0);
        }
        StartCoroutine(MoveRoutine());
	}
	
	// Update is called once per frame
	void Update ()
    {

    }

    IEnumerator MoveRoutine()
    {
        yield return new WaitForSeconds(.35f);
        if (TestCharController.player.GetComponent<TestCharController>().north)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1) * .1f;
        }
        else if (TestCharController.player.GetComponent<TestCharController>().south)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1) * .1f;
        }
        else if (TestCharController.player.GetComponent<TestCharController>().east)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0) * .1f;
        }
        else if (TestCharController.player.GetComponent<TestCharController>().west)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0) * .1f;
        }
        while (decayTime > 0)
        {
            for(int i= 0; i < 5; i++)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Fireball_2"), new Vector2(transform.position.x, transform.position.y), 
                    Quaternion.Euler(0,0,(Random.Range(0,360) - 90)));
                yield return new WaitForSeconds(.2f);
            }
            decayTime -= 1;
        }
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //print("Enter");
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //print("Exit");
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);

        }
    }

}
