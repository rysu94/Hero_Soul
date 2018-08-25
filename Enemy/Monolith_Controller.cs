using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monolith_Controller : MonoBehaviour
{

    public bool active = false;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(MonoRoutine());
        StartCoroutine(MonoAttack());
        GetComponent<Animator>().Play("Blue_Idle");
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator MonoRoutine()
    {
        while(true)
        {
            while(GameController.paused)
            {
                yield return null;
            }
            yield return new WaitForSeconds(Random.Range(5f, 10f));
            GetComponent<Animator>().Play("Red_Idle");
            active = true;
            yield return new WaitForSeconds(Random.Range(5f, 10f));
            GetComponent<Animator>().Play("Blue_Idle");
            active = false;
        }
    }

    IEnumerator MonoAttack()
    {
        while(true)
        {
            yield return new WaitForSeconds(.5f);
            while (GameController.paused)
            {
                yield return null;
            }
            if (active && TestCharController.player.GetComponent<Rigidbody2D>().velocity.magnitude > 0)
            {
                Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/Orbo_Missile"), transform.position, Quaternion.identity);
            }
        }
    }
}
