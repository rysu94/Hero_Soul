using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    bool active = false;
	// Use this for initialization
	void Start ()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -3);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Wall")
        {
            if(!active)
            {
                StartCoroutine(DestroyRoutine());
            }
        }
        else if(collider.tag == "Player")
        {
            DamageManager.PlayerDamage(10, collider.gameObject, false);
            TestCharController.playerAttack.clip = TestCharController.hurtNoise[Random.Range(0, TestCharController.hurtNoise.Length)];
            TestCharController.playerAttack.Play();
            Destroy(gameObject);
        }
        else if(collider.tag == "Enemy")
        {
            int tempInt = DamageManager.MagicDamage(collider.gameObject, 10);
            collider.gameObject.GetComponent<Monster>().DamageMonster(tempInt);
            Destroy(gameObject);
        }
    }

    IEnumerator DestroyRoutine()
    {
        active = true;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
