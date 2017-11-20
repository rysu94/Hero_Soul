using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chomper_Attack : MonoBehaviour
{
    public GameObject telegraph;
    public GameObject tentacle;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(AttackRoutine());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(3f);
        telegraph.SetActive(false);
        tentacle.SetActive(true);
        tentacle.GetComponent<Animator>().Play("Tentacle_Spawn");
        yield return new WaitForSeconds(3f);
        tentacle.GetComponent<Animator>().Play("Tentacle_Despawn");
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }
}
