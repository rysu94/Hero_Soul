using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enwater : StateController
{

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(StateDecayRoutine());
        StartCoroutine(EnwaterTickRoutine());
    }
	
    IEnumerator EnwaterTickRoutine()
    {
        PlayerStats.enwaterBonus = stackSize;
        while (stateTime > 0)
        {
            //Check if the object is paused
            while (GameController.paused)
            {
                yield return null;
            }

            yield return new WaitForSeconds(1f);

            StateManager.playerStates[stateIndex].stateTick++;
            stateTick = StateManager.playerStates[stateIndex].stateTick;
            if (stateTick == (8 - PlayerStats.enwaterBonus))
            {
                GameObject.Find("ManaNoise").GetComponent<AudioSource>().Play();
                PlayerStats.mana++;
                StateManager.playerStates[stateIndex].stateTick = 0;
                if (PlayerStats.mana > PlayerStats.maxMana)
                {
                    PlayerStats.mana = PlayerStats.maxMana;
                }
            }
        }

        PlayerStats.enwaterBonus = 0;
    }
}
