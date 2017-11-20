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
        Instantiate(Resources.Load("Prefabs/SpellFX/Explode_1_Large"), new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        Destroy(this.gameObject);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Wall" || other.gameObject.tag == "Enemy")
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Blaze_Explode"), new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            Instantiate(Resources.Load("Prefabs/SpellFX/Explode_1_Large"), new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
