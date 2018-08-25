using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enfire_State : StateController
{

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(StateDecayRoutine());
        StartCoroutine(EnfireTickRoutine());
    }
	

    IEnumerator EnfireTickRoutine()
    {
        PlayerStats.enfireBuffBonus = (int)(PlayerStats.strength * (0.25f * stackSize));
        while (stateTime > 0)
        {
            yield return new WaitForSeconds(1f);
        }

        PlayerStats.enfireBuffBonus = 0;
        
    }
}
