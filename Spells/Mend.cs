using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mend : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(MendRoutine());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator MendRoutine()
    {
        yield return new WaitForSeconds(.25f);
        PlayerStats.health += 50;
        if(PlayerStats.health > PlayerStats.maxHealth)
        {
            PlayerStats.health = PlayerStats.maxHealth;
        }
        yield return new WaitForSeconds(.85f);
        Destroy(gameObject);
    }
}
