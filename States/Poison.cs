using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : StateController
{
    public GameObject poisonPrefab;
    public static GameObject tempObj;

    // Use this for initialization
    void Start ()
    {
        StartCoroutine(StateDecayRoutine());
        StartCoroutine(PoisonTickRoutine());
	}
	
    //The Damage the poison state does, does more if the stack is higher
    IEnumerator PoisonTickRoutine()
    {
        Destroy(tempObj);
        tempObj = Instantiate(poisonPrefab, GameObject.Find("Player_States_Panel").transform);
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
            if (stateTick == 5)
            {
                DamageManager.PlayerDamage(5 * stackSize, TestCharController.player.gameObject, true);
                StateManager.playerStates[stateIndex].stateTick = 0;
            }    
        }
        Destroy(tempObj);
    }
}
