using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(HealRoutine());
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator HealRoutine()
    {
        yield return new WaitForSeconds(.25f);
        PlayerStats.health += 150;
        if (PlayerStats.health > PlayerStats.maxHealth)
        {
            PlayerStats.health = PlayerStats.maxHealth;
        }
        yield return new WaitForSeconds(1.15f);
        Destroy(gameObject);
    }
}
