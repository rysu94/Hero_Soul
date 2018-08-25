using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enliven_State : StateController
{

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(StateDecayRoutine());
        StartCoroutine(EnlivenTickRoutine());
    }

    IEnumerator EnlivenTickRoutine()
    {
        PlayerStats.staminaRegenBonus = stackSize;
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
            if (stateTick == 3)
            {
                PlayerStats.stamina += stackSize*5;
                StateManager.playerStates[stateIndex].stateTick = 0;
                if (PlayerStats.stamina > PlayerStats.maxStamina)
                {
                    PlayerStats.stamina = PlayerStats.maxStamina;
                }
            }
        }

        PlayerStats.staminaRegenBonus = 0;

    }

}
