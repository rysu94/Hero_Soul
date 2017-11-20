using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stoneskin : StateController
{

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(StateDecayRoutine());
        StartCoroutine(StoneskinTickRoutine());
    }
	
    IEnumerator StoneskinTickRoutine()
    {
        PlayerStats.defenseBuffBonus = (PotionController.stonePotionAmount * 10);
        while(stateTime > 0)
        {
            yield return new WaitForSeconds(1f);
        }
        PlayerStats.defenseBuffBonus = 0;
    }
}
