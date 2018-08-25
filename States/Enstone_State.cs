using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enstone_State : StateController
{

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(StateDecayRoutine());
        StartCoroutine(EnstoneTickRoutine());
    }
    
    IEnumerator EnstoneTickRoutine()
    {
        PlayerStats.enstoneBonus = (int)(PlayerStats.defense * (0.5f * stackSize));
        while (stateTime > 0)
        {
            yield return new WaitForSeconds(1f);
        }

        PlayerStats.enstoneBonus = 0;
    }
}
