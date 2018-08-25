using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaze : MonoBehaviour
{
    public Vector2 blazeVelocity;
    public Rigidbody2D blazeRB;


	// Use this for initialization
	void Start ()
    {
        DamageManager.totalHits = 0;
        blazeVelocity = TestCharController.playerVel;

        //Up
        if (TestCharController.player.GetComponent<TestCharController>().north)
        {
            blazeVelocity = new Vector2(0, 2);
        }
        //Left
        else if (TestCharController.player.GetComponent<TestCharController>().west)
        {
            blazeVelocity = new Vector2(-2, 0);
        }
        //Right
        else if (TestCharController.player.GetComponent<TestCharController>().east)
        {
            blazeVelocity = new Vector2(2, 0);
        }
        //Down
        else if (TestCharController.player.GetComponent<TestCharController>().south)
        {
            blazeVelocity = new Vector2(0, -2);
        }



        StartCoroutine(RemoveRoutine());

        blazeRB = GetComponent<Rigidbody2D>();
        blazeRB.velocity = blazeVelocity;

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator RemoveRoutine()
    {
        yield return new WaitForSeconds(2f);
        Instantiate(Resources.Load("Prefabs/SpellFX/Blaze_Explode"), new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        Instantiate(Resources.Load("Prefabs/SpellFX/Blaze_Explode"), new Vector2(transform.position.x, transform.position.y), Quaternion.Euler(0,0,22.5f));
        Instantiate(Resources.Load("Prefabs/SpellFX/Explode_1_Large"), new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        Destroy(this.gameObject);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Wall" || other.gameObject.tag == "Enemy")
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Blaze_Explode"), new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            Instantiate(Resources.Load("Prefabs/SpellFX/Blaze_Explode"), new Vector2(transform.position.x, transform.position.y), Quaternion.Euler(0, 0, 22.5f));
            Instantiate(Resources.Load("Prefabs/SpellFX/Explode_1_Large"), new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
