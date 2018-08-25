using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Froggy_Controller : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(FroggyRoutine());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator FroggyRoutine()
    {
        while(true)
        {
            while(GameController.paused)
            {
                yield return null;
            }
            yield return new WaitForSeconds(Random.Range(3, 6));
            GetComponent<Animator>().Play("Frog_Attack");
            Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/Orbo_Missile"), transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
            GetComponent<Animator>().Play("Frog_Idle");
        }
    }
}
